using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class ArtifactService : IArtifactService
    {
        private readonly IArtifactRepository _repo;
        public ArtifactService(IArtifactRepository repo) =>
            (_repo) = (repo);

        public async Task<Artifact> CreateArtifact(Artifact newArtifact)
        {
            var artifacts = await _repo.GetAllArtifacts();
            newArtifact.Id = artifacts.Max(x => x.Id) + 1;
            await _repo.CreateArtifact(newArtifact);
            return newArtifact;
        }

        public Task<List<Artifact>> GetAllArtifacts() =>
            _repo.GetAllArtifacts();

        public async Task<Artifact> UpdateArtifact(Artifact updatedArtifact)
        {
            var artifacts = await _repo.GetAllArtifacts();
            if (!artifacts.Any(x => x.Id == updatedArtifact.Id))
                throw new GenshinException(GenshinMessages.ArtifactNotFound);
            await _repo.UpdateArtifact(updatedArtifact);
            return updatedArtifact;
        }
    }
}