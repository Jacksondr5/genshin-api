using Core;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("team")]
    public class TeamController : ControllerBase
    {
        private readonly GenericCrudService<Team> _service;
        public TeamController(IGenericCrudRepository<Team> repo)
        {
            _service = new GenericCrudService<Team>(repo);
        }

        [HttpDelete("{teamId}")]
        public Task<Team> Delete([FromRoute] int teamId) =>
            _service.Delete(teamId);

        [HttpGet]
        public Task<List<Team>> GetAll() => _service.GetAll();

        [HttpPost]
        public Task<Team> Post([FromBody] Team newTeam) =>
            _service.Create(newTeam);

        [HttpPut]
        public Task<Team> Put([FromBody] Team Team) => _service.Update(Team);
    }
}