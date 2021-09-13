using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(
            IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IEnumerable<User>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _userRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User> CreateAsync(User userModel)
        {
            return await _userRepository.CreateAsync(userModel);
        }

        public async Task<User> EditAsync(User userModel)
        {
            return await _userRepository.EditAsync(userModel);
        }

        public async Task DeleteAsync(int id)
        {
            await _userRepository.DeleteAsync(id);
        }

        public async Task<bool> IsCpfValidAsync(string cpf, int id)
        {
            if (string.IsNullOrWhiteSpace(cpf))
            {
                return false;
            }

            var userModel = await _userRepository.GetCpfAsync(cpf, id);

            return userModel == null;
        }
    }
}
