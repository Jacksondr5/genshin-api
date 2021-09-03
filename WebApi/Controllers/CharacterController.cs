using Core;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("character")]
    public class CharacterController : ControllerBase
    {
        private readonly CharacterService _service;
        public CharacterController(IGenericCrudRepository<Character> repo)
        {
            _service = new CharacterService(repo);
        }

        [HttpGet]
        public Task<List<Character>> GetAllCharacters() => _service.GetAll();

        [HttpPost]
        public Task<Character> AddCharacter([FromBody] Character newCharacter) =>
            _service.Create(newCharacter);

        [HttpPost("{characterId}/Loadout")]
        public Task<Loadout> AddLoadoutToCharacter(
            [FromRoute] int characterId,
            [FromBody] Loadout newLoadout
        ) => _service.AddLoadoutToCharacter(characterId, newLoadout);

        [HttpPut("{characterId}/Loadout")]
        public Task<Loadout> UpdateLoadout(
            [FromRoute] int characterId,
            [FromBody] Loadout updatedLoadout
        ) => _service.UpdateLoadout(characterId, updatedLoadout);
    }
}