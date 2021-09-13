using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<User> GetByIdAsync(int id);
        Task<User> CreateAsync(User user);
        Task<User> EditAsync(User user);
        Task DeleteAsync(int id);
        Task<User> GetCpfAsync(string cpf, int id);
    }
}
