using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Domain.Model.Interfaces.Repositories;
using Domain.Model.Interfaces.Services;
using Domain.Model.Models;

namespace Domain.Service.Services
{
    public class DonationService : IDonationService
    {
        private readonly IDonationRepository _donationRepository;


        public DonationService(
            IDonationRepository donationRepository)
        {
            _donationRepository = donationRepository;
 
        }

        public async Task<IEnumerable<Donation>> GetAllAsync(bool orderAscendant, string search = null)
        {
            return await _donationRepository.GetAllAsync(orderAscendant, search);
        }

        public async Task<Donation> GetByIdAsync(int id)
        {
            return await _donationRepository.GetByIdAsync(id);
        }

        public async Task<Donation> CreateAsync(Donation donation)
        {
            return await _donationRepository.CreateAsync(donation);
        }

        public async Task<Donation> EditAsync(Donation donation)
        {
            return await _donationRepository.EditAsync(donation);
        }

        public async Task DeleteAsync(int id)
        {
            await _donationRepository.DeleteAsync(id);
        }

        public async Task<bool> IsZipCodeValidAsync(string donationZipCode, int id)
        {
            if (string.IsNullOrWhiteSpace(donationZipCode))
            {
                return false;
            }

            var donationModel = await _donationRepository.GetZipCodeAsync(donationZipCode, id);

            return donationModel == null;
        }


    }
}
