using Core;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [Route("character")]
    public class CharacterController : ControllerBase
    {
        private readonly ICharacterService _service;
        public CharacterController(ICharacterService service) =>
            (_service) = (service);

        [HttpGet]
        public Task<List<Character>> GetAllCharacters() =>
            _service.GetAllCharacters();

        [HttpPost]
        public Task<Character> AddCharacter([FromBody] Character newCharacter) =>
            _service.CreateCharacter(newCharacter);

        [HttpPost("{characterId}/Loadout")]
        public Task<Loadout> AddLoadoutToCharacter(
            [FromRoute] int characterId,
            [FromBody] Loadout newLoadout
        ) => _service.AddLoadoutToCharacter(characterId, newLoadout);

        [HttpPut("{characterId}/Loadout/{loadoutId}")]
        public Task<Loadout> UpdateLoadout(
            [FromRoute] int characterId,
            [FromBody] Loadout updatedLoadout
        ) => _service.UpdateLoadout(characterId, updatedLoadout);
    }
}