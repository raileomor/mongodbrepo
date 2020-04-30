using System;
using System.Net.Http;
using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Interfaces;
using MongoDbTest.Infrastructure.Models;
using Newtonsoft.Json;

namespace MongoDbTest.Infrastructure.RestClients
{
    public class AccountApiClient : IAccountApiClient
    {
        private readonly HttpClient _httpClient;
        private const string DefaultRequestMediaType = "application/json";

        public AccountApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("http://localhost:5000/");
            _httpClient.DefaultRequestHeaders.Add("ContentType", DefaultRequestMediaType);
        }

        public async Task<Account> GetAccountByIdAsync(string id)
        {
            Account response = null;

            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"accounts/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                string data = await responseMessage.Content.ReadAsStringAsync();
                response = !string.IsNullOrEmpty(data) ? JsonConvert.DeserializeObject<Account>(data) : null;
            }
            else
            {
                try
                {
                    responseMessage.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {
                    response = null;
                }
            }

            return response;
        }
    }
}