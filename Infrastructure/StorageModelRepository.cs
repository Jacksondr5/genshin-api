using System.Net.Http;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure
{
    public class StorageModelRepository
    {
        private readonly string _route;
        private readonly HttpClient _client;
        public StorageModelRepository(IConfiguration config)
        {
            _route = config["DumbStorageUrl"];
            _client = new HttpClient();
        }

        internal async Task<StorageModel> GetStorageModel()
        {
            var response = await _client.GetAsync($"{_route}/genshin");
            if (!response.IsSuccessStatusCode)
                throw new GenshinException(
                    $"Response from dumb storage API not OK.  {response.ReasonPhrase}"
                );
            var content = await response.Content.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<StorageModel>(content);
            if (model == null)
                throw new GenshinException("The storage model is null");
            return model;
        }

        internal Task UpdateStorageModel(StorageModel model) =>
            _client.PostAsync(
                $"{_route}/genshin",
                new StringContent(JsonConvert.SerializeObject(model))
            );
    }
}