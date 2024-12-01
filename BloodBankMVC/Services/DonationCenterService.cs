using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using BloodBank.DTOs;
using BloodBank.Models;
using BloodBankMVC.Models;

namespace BloodBankMVC.Services
{
    public class DonationCenterService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://bloodb-recip-tvz91patwhf6-481634796.ca-central-1.elb.amazonaws.com/api/DonationCenter";

        public DonationCenterService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<DonationCenter>> GetAllDonationCentersAsync()
        {
            var response = await _httpClient.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<DonationCenter>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<DonationCenter> GetDonationCenterByIdAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<DonationCenter>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        public async Task<bool> AddDonationCenterAsync(DonationCenter donationCenter)
        {
            var jsonRequest = JsonSerializer.Serialize(donationCenter);
            var response = await _httpClient.PostAsync(BaseUrl, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateDonationCenterAsync(string id, DonationCenter donationCenter)
        {
            var jsonRequest = JsonSerializer.Serialize(donationCenter);
            var response = await _httpClient.PutAsync($"{BaseUrl}/{id}", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> UpdateHoursOfOperationAsync(string id, DonationCenter hoursOfOperation)
        {
            var jsonRequest = JsonSerializer.Serialize(new { HoursOfOperation = hoursOfOperation });
            var response = await _httpClient.PatchAsync($"{BaseUrl}/{id}", new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteDonationCenterAsync(string id)
        {
            var response = await _httpClient.DeleteAsync($"{BaseUrl}/{id}");
            return response.IsSuccessStatusCode;
        }
    }
}
