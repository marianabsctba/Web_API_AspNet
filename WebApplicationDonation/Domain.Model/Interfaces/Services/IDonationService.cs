using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Domain.Model.Models;

namespace Domain.Model.Interfaces.Services
{
    public interface IDonationService
    {
        Task<IEnumerable<Donation>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<Donation> GetByIdAsync(int id);
        Task<Donation> CreateAsync(Donation donation);
        Task<Donation> EditAsync(Donation donation);
        Task DeleteAsync(int id);
        Task<bool> IsZipCodeValidAsync(string donationZipCode, int id);
    
    }
}
