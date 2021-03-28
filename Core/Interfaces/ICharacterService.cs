using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface ICharacterService
    {
        Task<Loadout> AddLoadoutToCharacter(int characterId, Loadout newLoadout);
        Task<Character> CreateCharacter(Character newCharacter);
        Task<List<Character>> GetAllCharacters();
        Task<Loadout> UpdateLoadout(int characterId, Loadout updatedLoadout);
    }
}