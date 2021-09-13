using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(User user);
        Task<User> EditAsync(User user);
        Task DeleteAsync(int id);
        Task<bool> IsCpfValidAsync(string cpf, int id);
    }
}
