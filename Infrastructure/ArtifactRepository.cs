using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace Infrastructure
{
    public class ArtifactRepository : IArtifactRepository
    {
        private readonly StorageModelRepository _storageModelRepo;
        public ArtifactRepository(StorageModelRepository storageModelRepo) =>
            (_storageModelRepo) = (storageModelRepo);

        public async Task CreateArtifact(Artifact newArtifact)
        {
            var model = await _storageModelRepo.GetStorageModel();
            model.Artifacts.Add(newArtifact);
            await _storageModelRepo.UpdateStorageModel(model);
        }

        public async Task DeleteArtifact(int artifactId)
        {
            var model = await _storageModelRepo.GetStorageModel();
            model.Artifacts.RemoveAll(x => x.Id == artifactId);
            await _storageModelRepo.UpdateStorageModel(model);
        }

        public async Task<List<Artifact>> GetAllArtifacts()
        {
            var model = await _storageModelRepo.GetStorageModel();
            return model.Artifacts;
        }

        public async Task UpdateArtifact(Artifact updatedArtifact)
        {
            var model = await _storageModelRepo.GetStorageModel();
            var artifact =
                model.Artifacts.First(x => x.Id == updatedArtifact.Id);
            artifact = updatedArtifact;
            await _storageModelRepo.UpdateStorageModel(model);
        }
    }
}