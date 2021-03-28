using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface IArtifactRepository
    {
        Task CreateArtifact(Artifact newArtifact);
        Task<List<Artifact>> GetAllArtifacts();
        Task UpdateArtifact(int artifactId, Artifact updatedArtifact);
    }
}