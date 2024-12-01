using System.Text.Json;
using System.Text;
using BloodBank.Models;
using BloodBankMVC.Models;
using BloodBank.DTOs;

namespace BloodBankMVC.Services
{
    public class BloodTypeService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "http://bloodb-recip-tvz91patwhf6-481634796.ca-central-1.elb.amazonaws.com/api/BloodType";

        public BloodTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all blood types for a specific area (e.g., Donation Center)
        public async Task<IEnumerable<BloodTypeInfo>> GetAllBloodTypesAsync(string id)
        {
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<BloodTypeInfo>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        /*
         * var response = await _httpClient.GetAsync(BaseUrl);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<IEnumerable<DonationCenter>>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }
         */

        // Get details of a specific blood type
        public async Task<BloodTypeInfo> GetBloodTypeByIdAsync(string id, string bloodTypeId)
        {
            var url = $"{BaseUrl}/{id}";
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<BloodTypeInfo>(jsonResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
        }

        // Add a new blood type to a donation center
        public async Task<bool> AddBloodTypeAsync(string id, BloodTypeInfo model)
        {
            var url = $"{BaseUrl}/{id}";
            var jsonRequest = JsonSerializer.Serialize(model);
            var response = await _httpClient.PostAsync(url, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        // Update a blood type in a donation center
        public async Task<bool> UpdateBloodTypeAsync(string id, BloodTypeInfo model)
        {
            var url = $"{BaseUrl}/{id}/{model.Id}";
            var jsonRequest = JsonSerializer.Serialize(model);
            var response = await _httpClient.PutAsync(url, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        // Update stock level (partial update)
        public async Task<bool> UpdateBloodTypeStockLevelAsync(string id, string bloodTypeId, int stockLevel)
        {
            var url = $"{BaseUrl}/{id}/{bloodTypeId}";
            var jsonRequest = JsonSerializer.Serialize(new { StockLevel = stockLevel });
            var response = await _httpClient.PatchAsync(url, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        // Delete a specific blood type from a donation center
        public async Task<bool> DeleteBloodTypeAsync(string id, string bloodTypeId)
        {
            var url = $"{BaseUrl}/{id}/{bloodTypeId}";
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
