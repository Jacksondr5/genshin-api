using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface IArtifactService
    {
        Task<Artifact> CreateArtifact(Artifact newArtifact);
        Task<Artifact> DeleteArtifact(int artifactId);
        Task<List<Artifact>> GetAllArtifacts();
        Task<Artifact> UpdateArtifact(Artifact updatedArtifact);
    }
}