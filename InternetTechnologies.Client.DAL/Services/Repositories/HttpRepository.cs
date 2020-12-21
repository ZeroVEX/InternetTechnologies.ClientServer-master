using InternetTechnologies.DomainModels.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace InternetTechnologies.Client.DAL.Services.Repositories
{
    internal class HttpRepository<T> : IRepository<T>
        where T : class, IEntity
    {
        private readonly HttpClient _httpClient = new HttpClient();

        private readonly string _apiPath = $"api/{typeof(T).Name}";

        public HttpRepository(string baseAddress)
        {
            _httpClient.BaseAddress = new Uri(baseAddress);
        }

        public HttpRepository(string baseAddress, string apiPath)
            : this(baseAddress)
        {
            _apiPath = apiPath;
        }

        public async Task CreateAsync(T item)
        {
            await _httpClient.PostAsync(_apiPath, JsonContent.Create(item));

            item.Id = (await GetCollectionAsync()).Last().Id;
        }

        public async Task<T> ReadAsync(int id)
        {
            string getQuery = string.Join('/', _apiPath, id);

            var response = await _httpClient.GetAsync(getQuery);

            string jsonData = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<T>(jsonData);
        }

        public async Task UpdateAsync(T item)
        {
            string putQuery = string.Join('/', _apiPath, item.Id);

            await _httpClient.PutAsync(putQuery, JsonContent.Create(item));
        }

        public async Task DeleteAsync(int id)
        {
            string deleteQuery = string.Join('/', _apiPath, id);

            await _httpClient.DeleteAsync(deleteQuery);
        }

        public async Task<IEnumerable<T>> GetCollectionAsync()
        {
            var response = await _httpClient.GetAsync(_apiPath);

            string jsonData = await response.Content.ReadAsStringAsync();

            return JsonConvert.DeserializeObject<IEnumerable<T>>(jsonData);
        }
    }
}
