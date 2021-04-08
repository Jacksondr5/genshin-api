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
            var artifacts = await GetAllArtifacts();
            newArtifact.Id = artifacts.Max(x => x.Id) + 1;
            await _repo.CreateArtifact(newArtifact);
            return newArtifact;
        }

        public async Task<Artifact> DeleteArtifact(int artifactId)
        {
            var artifacts = await GetAllArtifacts();
            var artifact = artifacts.SingleOrDefault(x => x.Id == artifactId);
            if (artifact == null)
                throw new GenshinException(GenshinMessages.ArtifactNotFound);
            await _repo.DeleteArtifact(artifactId);
            return artifact;
        }

        public Task<List<Artifact>> GetAllArtifacts() =>
            _repo.GetAllArtifacts();

        public async Task<Artifact> UpdateArtifact(Artifact updatedArtifact)
        {
            var artifacts = await GetAllArtifacts();
            if (!artifacts.Any(x => x.Id == updatedArtifact.Id))
                throw new GenshinException(GenshinMessages.ArtifactNotFound);
            await _repo.UpdateArtifact(updatedArtifact);
            return updatedArtifact;
        }
    }
}