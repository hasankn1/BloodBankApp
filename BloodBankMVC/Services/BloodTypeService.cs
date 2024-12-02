using System.Text.Json;
using System.Text;
using BloodBank.Models;
using BloodBankMVC.Models;
using BloodBank.DTOs;
using System.Text.Json.Serialization;
using System.Linq;
using System.Net.Http.Json;
using Newtonsoft.Json;

namespace BloodBankMVC.Services
{
    public class BloodTypeService
    {
        private readonly HttpClient _httpClient;
        //private const string BaseUrl = "http://bloodb-recip-ktimuugbml5i-469788863.ca-central-1.elb.amazonaws.com/api/BloodType";
        private const string BaseUrl = "http://localhost:5031/api/BloodType";
        public BloodTypeService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // Get all blood types for a specific area (e.g., Donation Center)
        //public async Task<IEnumerable<BloodTypeInfo>> GetAllBloodTypesAsync(string id)
        //{
        //    var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
        //    response.EnsureSuccessStatusCode();

        //    var jsonResponse = await response.Content.ReadAsStringAsync();
        //    var bloodType = JsonSerializer.Deserialize<IEnumerable<BloodTypeInfo>>(jsonResponse, new JsonSerializerOptions
        //    { 
        //        PropertyNameCaseInsensitive = true,
        //        Converters = { new JsonStringEnumConverter() }
        //    });
        //    return bloodType;
        //}
        public async Task<IEnumerable<BloodTypeInfo>> GetAllBloodTypesAsync(string id)
        {
            // Call to the API to retrieve donation center data
            var response = await _httpClient.GetAsync($"{BaseUrl}/{id}");
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine(jsonResponse); // Log the JSON response

            // Deserialize the response directly to a List<BloodTypeInfo>
            var bloodTypes = Newtonsoft.Json.JsonConvert.DeserializeObject<List<BloodTypeInfo>>(jsonResponse);

            return bloodTypes ?? new List<BloodTypeInfo>(); // Return the deserialized list or an empty list
        }



        // Get details of a specific blood type
        public async Task<BloodTypeInfo> GetBloodTypeByIdAsync(string id, string bloodTypeId)
        {
            var url = $"{BaseUrl}/{id}"; // Make sure your endpoint returns a collection or filtered data
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine("JSON Response: " + jsonResponse);

            var bloodTypes = JsonConvert.DeserializeObject<List<BloodTypeInfo>>(jsonResponse);

            // Find the specific blood type that matches bloodTypeId
            var bloodTypeDetails = bloodTypes.FirstOrDefault(bt => bt.Type == bloodTypeId);

            return bloodTypeDetails;
        }



        // Add a new blood type to a donation center
        public async Task<bool> AddBloodTypeAsync(string id, BloodTypeInfo model)
        {
            var url = $"{BaseUrl}/{id}";
            var jsonRequest = System.Text.Json.JsonSerializer.Serialize(model);
            var response = await _httpClient.PostAsync(url, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));
            Console.WriteLine($"Saving BloodType to repository: {jsonRequest}");
            Console.WriteLine($"Saving BloodType to repository: {response}");
            return response.IsSuccessStatusCode;
        }

        // Update a blood type in a donation center
        public async Task<bool> UpdateBloodTypeAsync(string id, BloodTypeInfo model)
        {
            var url = $"{BaseUrl}/{id}/{model.Id}";
            var jsonRequest = System.Text.Json.JsonSerializer.Serialize(model);
            var response = await _httpClient.PutAsync(url, new StringContent(jsonRequest, Encoding.UTF8, "application/json"));

            return response.IsSuccessStatusCode;
        }

        // Update stock level (partial update)
        public async Task<bool> UpdateBloodTypeStockLevelAsync(string id, string bloodTypeId, int stockLevel)
        {
            var url = $"{BaseUrl}/{id}/{bloodTypeId}";
            var jsonRequest = System.Text.Json.JsonSerializer.Serialize(new { StockLevel = stockLevel });
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
