using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationDonation.Models;

namespace WebApplicationDonation.Services
{
    public interface IUserHttpService
    {
        Task<IEnumerable<UserViewModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<UserViewModel> GetByIdAsync(int id);
        Task<UserViewModel> CreateAsync(UserViewModel userViewModel);
        Task<UserViewModel> EditAsync(UserViewModel userViewModel);
        Task DeleteAsync(int id);
        Task<bool> IsCpfValidAsync(string cpf, int id);
    }
}
