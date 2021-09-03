using Core;
using Core.Interfaces;
using Core.Services;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("artifact")]
    public class ArtifactController : ControllerBase
    {
        private readonly GenericCrudService<Artifact> _service;
        public ArtifactController(IGenericCrudRepository<Artifact> repo)
        {
            _service = new GenericCrudService<Artifact>(repo);
        }

        [HttpDelete("{artifactId}")]
        public Task<Artifact> Delete([FromRoute] int artifactId) =>
            _service.Delete(artifactId);

        [HttpGet]
        public Task<List<Artifact>> GetAll() => _service.GetAll();

        [HttpPost]
        public Task<Artifact> Post([FromBody] Artifact newArtifact) =>
            _service.Create(newArtifact);

        [HttpPut]
        public Task<Artifact> Put([FromBody] Artifact artifact) =>
            _service.Update(artifact);
    }
}