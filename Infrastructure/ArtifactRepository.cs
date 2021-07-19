using Core;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class ArtifactRepository : IArtifactRepository
    {
        private readonly IMongoCollection<Artifact> _collection;
        public ArtifactRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Artifact>("artifacts");
        }

        public Task CreateArtifact(Artifact newArtifact) =>
            _collection.InsertOneAsync(newArtifact);

        public Task DeleteArtifact(int artifactId) =>
            _collection.DeleteOneAsync(x => x.Id == artifactId);

        public Task<List<Artifact>> GetAllArtifacts() =>
            _collection.Find(_ => true).ToListAsync();

        public Task<int?> GetMaxId() =>
            InfrastructureUtils.GetMaxId<Artifact>(_collection);

        public Task UpdateArtifact(Artifact updatedArtifact) =>
            _collection.ReplaceOneAsync(
                x => x.Id == updatedArtifact.Id,
                updatedArtifact
            );
    }
}