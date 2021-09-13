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
    public class DonationRepository : IDonationRepository
    {
        private readonly WebDbContext _context;

        public DonationRepository(
            WebDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Donation>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var donations = orderAscendant
                ? _context.Donations.OrderBy(x => x.DonationName)
                : _context.Donations.OrderByDescending(x => x.DonationName);

            if (string.IsNullOrWhiteSpace(search))
            {
                return await donations
                    .Include(x => x.User)
                    .ToListAsync();
            }

            return await donations
                .Include(x => x.User)
                .Where(x => x.DonationName.Contains(search))
                .ToListAsync();
        }

        public async Task<Donation> GetByIdAsync(int id)
        {
            var donation = await _context
                .Donations
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);

            return donation;
        }

        public async Task<Donation> CreateAsync(Donation donationModel)
        {
            var donation = _context.Donations.Add(donationModel);

            await _context.SaveChangesAsync();

            return donation.Entity;
        }

        public async Task<Donation> EditAsync(Donation donationModel)
        {
            var donation = _context.Donations.Update(donationModel);

            await _context.SaveChangesAsync();

            return donation.Entity;
        }

        public async Task DeleteAsync(int id)
        {
            var donation = await GetByIdAsync(id);

            _context.Donations.Remove(donation);

            await _context.SaveChangesAsync();
        }

        public async Task<Donation> GetZipCodeAsync(string donationZipCode, int id)
        {
            var donationModel = await _context
                .Donations
                .FirstOrDefaultAsync(x => x.DonationZipCode == donationZipCode && x.Id != id);

            return donationModel;

        }
    }
}
