using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;

namespace Infrastructure
{
    public class CharacterRepository : ICharacterRepository
    {
        private readonly StorageModelRepository _storageModelRepo;
        public CharacterRepository(StorageModelRepository storageModelRepo) =>
            (_storageModelRepo) = (storageModelRepo);

        public async Task CreateCharacter(Character newCharacter)
        {
            var model = await _storageModelRepo.GetStorageModel();
            model.Characters.Add(newCharacter);
            await _storageModelRepo.UpdateStorageModel(model);
        }

        public async Task<List<Character>> GetAllCharacters()
        {
            var model = await _storageModelRepo.GetStorageModel();
            return model.Characters;
        }

        public async Task UpdateCharacter(Character updatedCharacter)
        {
            var model = await _storageModelRepo.GetStorageModel();
            var character =
                model.Characters.First(x => x.Id == updatedCharacter.Id);
            character = updatedCharacter;
            await _storageModelRepo.UpdateStorageModel(model);
        }
    }
}