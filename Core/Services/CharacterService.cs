using Core.Exceptions;
using Core.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Services
{
    public class CharacterService : GenericCrudService<Character>
    {
        public CharacterService(IGenericCrudRepository<Character> repo)
            : base(repo) { }

        public async Task<Loadout> AddLoadoutToCharacter(
            int characterId,
            Loadout newLoadout
        )
        {
            var character = await Get(characterId);
            newLoadout.Id = character.Loadouts.Max(x => x.Id) + 1;
            character.Loadouts.Add(newLoadout);
            await _repo.Update(character);
            return newLoadout;
        }

        public override Task<Character> Update(Character updatedEntity)
        {
            //TODO: figure out how to do this
            throw new InvalidOperationException("Cannot update a character");
        }

        public async Task<Loadout> UpdateLoadout(
            int characterId,
            Loadout updatedLoadout
        )
        {
            var character = await Get(characterId);
            var index = character.Loadouts.FindIndex(
                x => x.Id == updatedLoadout.Id
            );
            if (index == -1)
                throw new DataNotFoundException<Loadout>(updatedLoadout.Id);
            character.Loadouts[index] = updatedLoadout;
            await _repo.Update(character);
            return updatedLoadout;
        }
    }
}