using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using WebApplicationDonation.Models;

namespace WebApplicationDonation.Services
{
    public interface IDonationHttpService
    {
        Task<IEnumerable<DonationViewModel>> GetAllAsync(
            bool orderAscendant,
            string search = null);
        Task<DonationViewModel> GetByIdAsync(int id);
        Task<DonationViewModel> CreateAsync(DonationViewModel donationViewModel);
        Task<DonationViewModel> EditAsync(DonationViewModel donationViewModel);
        Task DeleteAsync(int id);
        Task<bool> IsZipCodeValidAsync(string donationZipCode, int id);
    }
}
