using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface ICharacterRepository
    {
        Task CreateCharacter(Character newCharacter);
        Task<List<Character>> GetAllCharacters();
        Task UpdateCharacter(Character updatedCharacter);
    }
}