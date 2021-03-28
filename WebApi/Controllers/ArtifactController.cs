using System.Collections.Generic;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("artifact")]
    public class ArtifactController : ControllerBase
    {
        private readonly IArtifactService _service;
        public ArtifactController(IArtifactService service) =>
            (_service) = (service);

        [HttpGet]
        public Task<List<Artifact>> GetAll() =>
            _service.GetAllArtifacts();

        [HttpPost]
        public Task<Artifact> Post([FromBody] Artifact newArtifact) =>
            _service.CreateArtifact(newArtifact);

        [HttpPut("{artifactId}")]
        public Task<Artifact> Put(
            [FromRoute] int artifactId,
            [FromBody] Artifact artifact
        ) => _service.UpdateArtifact(artifactId, artifact);
    }
}