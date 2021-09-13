using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Models;
using Infra.Data.Data;
using Microsoft.EntityFrameworkCore;

namespace Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly WebDbContext _context;

        public UserRepository(
            WebDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var users = _context.Users.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
            {
                users = users
                    .Where(x => x.UserName.Contains(search) || x.UserLastName.Contains(search));
            }

            users = orderAscendant
                ? users.OrderBy(x => x.UserName)
                : users.OrderByDescending(x => x.UserLastName);

            var result = await users
                .Select(x => new
                {
                    User = x,
                    Quantitydon = x.Donations.Count
                })
                .ToListAsync();

            var usersResult = result
                .Select(x =>
                {
                    x.User.QuantityDonations = x.Quantitydon;
                    return x.User;
                });

            return usersResult;
        }

        public async Task<User> GetByIdAsync(int id)
        {
            var userTask = await _context
                .Users
                .Include(x => x.Donations)
                .FirstOrDefaultAsync(x => x.Id == id);

            var quantityTask = await _context.Donations.CountAsync(x => x.UserId == id);

            var user = userTask;

            user.QuantityDonations = quantityTask;

            return user;
        }

        public async Task<User> CreateAsync(User userModel)
        {
            var user = _context.Users.Add(userModel);

            await _context.SaveChangesAsync();

            return user.Entity;
        }

        public async Task<User> EditAsync(User userModel)
        {
            var user = _context.Users.Update(userModel);

            await _context.SaveChangesAsync();

            return user.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var user = await GetByIdAsync(id);

            _context.Users.Remove(user);

            await _context.SaveChangesAsync();
        }

        public async Task<User> GetCpfAsync(string cpf, int id)
        {
            var userModel = await _context
                .Users
                .FirstOrDefaultAsync(x => x.Cpf == cpf && x.Id != id);

            return userModel;

        }
    }
}