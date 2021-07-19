using Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface IArtifactRepository : IBaseRepository
    {
        Task CreateArtifact(Artifact newArtifact);
        Task DeleteArtifact(int artifactId);
        Task<List<Artifact>> GetAllArtifacts();
        Task UpdateArtifact(Artifact updatedArtifact);
    }
}