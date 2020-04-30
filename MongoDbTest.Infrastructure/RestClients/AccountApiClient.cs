using System;
using System.Net.Http;
using System.Threading.Tasks;
using MongoDbTest.Infrastructure.Interfaces;

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

        public async Task<bool> ExistAsync(string id)
        {
            bool response = false;

            HttpResponseMessage responseMessage = await _httpClient.GetAsync($"accounts/{id}");

            if (responseMessage.IsSuccessStatusCode)
            {
                response = true;
            }
            else
            {
                try
                {
                    responseMessage.EnsureSuccessStatusCode();
                }
                catch (Exception)
                {
                    response = false;
                }
            }

            return response;
        }
    }
}