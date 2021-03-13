using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using WebApi.Model;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("artifact")]
    public class ArtifactController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<Artifact> GetAll()
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public Artifact Post([FromBody] Artifact newArtifact)
        {
            throw new NotImplementedException();
        }

        [HttpPut("{artifactId}")]
        public Artifact Put(
            [FromRoute] int artifactId,
            [FromBody] Artifact artifact
        )
        {
            throw new NotImplementedException();
        }
    }
}