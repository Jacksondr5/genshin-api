using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Core;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure
{
    public class ArtifactRepository : IArtifactRepository
    {
        private readonly string _route;
        private readonly HttpClient _client;
        public ArtifactRepository(IConfiguration config)
        {
            _route = config["dumb-storage-url"];
            _client = new HttpClient();
        }
        public async Task CreateArtifact(Artifact newArtifact)
        {
            var model = await GetStorageModel();
            model.Artifacts.Add(newArtifact);
            await UpdateStorageModel(model);
        }

        public async Task<List<Artifact>> GetAllArtifacts()
        {
            var model = await GetStorageModel();
            return model.Artifacts;
        }

        public async Task UpdateArtifact(
            int artifactId,
            Artifact updatedArtifact
        )
        {
            var model = await GetStorageModel();
            var artifact = model.Artifacts.First(x => x.Id == artifactId);
            artifact = updatedArtifact;
            await UpdateStorageModel(model);
        }

        private async Task<StorageModel> GetStorageModel()
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

        private Task UpdateStorageModel(StorageModel model) =>
            _client.PostAsync(
                $"{_route}/genshin",
                new StringContent(JsonConvert.SerializeObject(model))
            );
    }
}