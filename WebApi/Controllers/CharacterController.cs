using System;
using System.Collections.Generic;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    public class CharacterController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Character> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpPost("{characterId}/Loadout")]
        public Loadout AddLoadout(
            [FromRoute] int characterId,
            [FromBody] Loadout loadout
        )
        {
            throw new NotImplementedException();
        }

        [HttpPut("{characterId}")]
        public Loadout Put(
            [FromRoute] int characterId,
            [FromBody] Loadout loadout
        )
        {
            throw new NotImplementedException();
        }
    }
}