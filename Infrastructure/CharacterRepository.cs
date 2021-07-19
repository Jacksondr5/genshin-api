using Core;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly IMongoCollection<Character> _collection;
        public CharacterRepository(IMongoDatabase database)
        {
            _collection = database.GetCollection<Character>("characters");
        }

        public Task CreateCharacter(Character newCharacter) =>
            _collection.InsertOneAsync(newCharacter);

        public Task<List<Character>> GetAllCharacters() =>
            _collection.Find(_ => true).ToListAsync();

        public Task<int?> GetMaxId() =>
            InfrastructureUtils.GetMaxId<Character>(_collection);

        public Task UpdateCharacter(Character updatedCharacter) =>
            _collection.ReplaceOneAsync(
                x => x.Id == updatedCharacter.Id,
                updatedCharacter
            );
    }
}