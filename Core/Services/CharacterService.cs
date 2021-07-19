using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core
{
    public class CharacterService : ICharacterService
    {
        private readonly ICharacterRepository _repo;
        public CharacterService(ICharacterRepository repo) =>
            (_repo) = (repo);

        public async Task<Loadout> AddLoadoutToCharacter(
            int characterId,
            Loadout newLoadout
        )
        {
            var characters = await _repo.GetAllCharacters();
            var character = characters.FirstOrDefault(x => x.Id == characterId);
            if (character == null)
                throw new GenshinException(GenshinMessages.CharacterNotFound);
            newLoadout.Id = character.Loadouts.Max(x => x.Id) + 1;
            character.Loadouts.Add(newLoadout);
            await _repo.UpdateCharacter(character);
            return newLoadout;
        }

        public async Task<Character> CreateCharacter(Character newCharacter)
        {
            newCharacter.Id = (await _repo.GetMaxId() ?? 0) + 1;
            await _repo.CreateCharacter(newCharacter);
            return newCharacter;
        }

        public Task<List<Character>> GetAllCharacters() =>
            _repo.GetAllCharacters();

        public async Task<Loadout> UpdateLoadout(
            int characterId,
            Loadout updatedLoadout
        )
        {
            var characters = await _repo.GetAllCharacters();
            var character = characters.FirstOrDefault(x => x.Id == characterId);
            if (character == null)
                throw new GenshinException(GenshinMessages.CharacterNotFound);
            var loadout = character.Loadouts.FirstOrDefault(
                x => x.Id == updatedLoadout.Id
            );
            if (loadout == null)
                throw new GenshinException(GenshinMessages.LoadoutNotFound);
            loadout = updatedLoadout;
            await _repo.UpdateCharacter(character);
            return loadout;
        }
    }
}