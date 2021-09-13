using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using WebApplicationDonation.Models;

namespace WebApplicationDonation.Services.Implementations
{
    public class DonationHttpService : IDonationHttpService
    {
        private readonly HttpClient _httpClient;

        private static readonly JsonSerializerOptions JsonSerializerOptions = new()
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        };

        public DonationHttpService(
            HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<DonationViewModel> CreateAsync(DonationViewModel donationViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PostAsJsonAsync(string.Empty, donationViewModel);

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var donationCreated = await JsonSerializer
                .DeserializeAsync<DonationViewModel>(contentStream, JsonSerializerOptions);

            return donationCreated;
        }

        public async Task DeleteAsync(int id)
        {
            var httpResponseMessage = await _httpClient
                .DeleteAsync($"{id}");

            httpResponseMessage.EnsureSuccessStatusCode();
        }

        public async Task<DonationViewModel> EditAsync(DonationViewModel donationViewModel)
        {
            var httpResponseMessage = await _httpClient
                .PutAsJsonAsync($"{donationViewModel.Id}", donationViewModel);

            httpResponseMessage.EnsureSuccessStatusCode();

            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

            var donationEdited = await JsonSerializer
                .DeserializeAsync<DonationViewModel>(contentStream, JsonSerializerOptions);

            return donationEdited;
        }

        public async Task<IEnumerable<DonationViewModel>> GetAllAsync(bool orderAscendant, string search = null)
        {
            var donations = await _httpClient
                .GetFromJsonAsync<IEnumerable<DonationViewModel>>($"{orderAscendant}/{search}");

            return donations;
        }

        public async Task<DonationViewModel> GetByIdAsync(int id)
        {
            var donations = await _httpClient
                .GetFromJsonAsync<DonationViewModel>($"{id}");

            return donations;
        }

        public async Task<bool> IsZipCodeValidAsync(string donationZipCode, int id)
        {
            var isZipCodeValid = await _httpClient
                .GetFromJsonAsync<bool>($"IsZipCodeValid/{donationZipCode}/{id}");

            return isZipCodeValid;
        }
    }
}
