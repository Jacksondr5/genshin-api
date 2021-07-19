using Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core
{
    public interface ICharacterRepository : IBaseRepository
    {
        Task CreateCharacter(Character newCharacter);
        Task<List<Character>> GetAllCharacters();
        Task UpdateCharacter(Character updatedCharacter);
    }
}