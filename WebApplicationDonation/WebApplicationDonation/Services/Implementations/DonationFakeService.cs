using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebApplicationDonation.Models;

namespace WebApplicationDonation.Services.Implementations
{
    public class DonationFakeService : IDonationHttpService
    {
        private static List<DonationViewModel> Donations { get; } = new List<DonationViewModel>
        {
            new DonationViewModel
            {
                Id = 0,
                DonationName = "bolsa",
                Description = "bonita",
                DonationZipCode ="81720230",
                DateOfRegister = DateTime.Now,
                CourierPrice = 12.50,
                Quantity = 1,
                NewOrOld =  true

            },

            new DonationViewModel
            {
                Id = 1,
                DonationName = "bola",
                Description = "azul",
                DonationZipCode ="9192500023",
                DateOfRegister = new DateTime(2011, 01, 03),
                CourierPrice = 12.50,
                Quantity = 2,
                NewOrOld =  true
            }
        };

        public async Task<IEnumerable<DonationViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            if (search == null)
            {
                return Donations;
            }

            var resultByLinq = Donations
                .Where(x => x.DonationName.Contains(search, StringComparison.OrdinalIgnoreCase));

            resultByLinq = orderAscendant
                ? resultByLinq.OrderBy(x => x.DonationName)
                : resultByLinq.OrderByDescending(x => x.DonationName);

            return resultByLinq;
        }


        public async Task<DonationViewModel> GetByIdAsync(int id)
        {
            foreach (var donation in Donations)
            {
                if (donation.Id == id)
                {
                    return donation;
                }
            }

            return null;
        }

        private static int _id = Donations.Max(x => x.Id);
        private int Id => Interlocked.Increment(ref _id);

        public async Task<DonationViewModel> CreateAsync(DonationViewModel DonationViewModel)
        {
            DonationViewModel.Id = Id;

            Donations.Add(DonationViewModel);

            return DonationViewModel;
        }

        public async Task<DonationViewModel> EditAsync(DonationViewModel DonationViewModel)
        {
            foreach (var donation in Donations)
            {
                if (donation.Id == DonationViewModel.Id)
                {
                    donation.DonationName = DonationViewModel.DonationName;
                    donation.Description = DonationViewModel.Description;
                    donation.DonationZipCode = DonationViewModel.DonationZipCode;
                    donation.DateOfRegister = DonationViewModel.DateOfRegister;
                    donation.CourierPrice = DonationViewModel.CourierPrice;
                    donation.Quantity = DonationViewModel.Quantity;
                    donation.NewOrOld = DonationViewModel.NewOrOld;
                    donation.UserId = DonationViewModel.UserId;

                    return donation;
                }
            }

            return null;
        }

        public async Task DeleteAsync(int id)
        {
            DonationViewModel foundedDonation = null;
            foreach (var donation in Donations)
            {
                if (donation.Id == id)
                {
                    foundedDonation = donation;
                }
            }

            if (foundedDonation != null)
            {
                Donations.Remove(foundedDonation);
            }
        }

        public async Task<bool> IsZipCodeValidAsync(string donationZipCode, int id)
        {
            throw new NotImplementedException();
        }
    }
}
