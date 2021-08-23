using EnvironmentService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnvironmentsController : ControllerBase
    {
        private readonly IEnvironmentRepository _environmentRepository;

        public EnvironmentsController(IEnvironmentRepository environmentRepository)
        {
            this._environmentRepository = environmentRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Models.Environment>> GetEnvironments()
        {
            return await this._environmentRepository.Get();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Models.Environment>> GetEnvironments(int id)
        {
            var environment = await this._environmentRepository.Get(id);

            if (environment == null)
                return NotFound("Not found!");

            return environment;
        }

        [HttpPost]
        public async Task<ActionResult<Models.Environment>> PostEnvironments([FromBody] Models.Environment environment)
        {
            var newEnvironment = await this._environmentRepository.Create(environment);
            return CreatedAtAction(nameof(GetEnvironments), new { id = newEnvironment.Id }, newEnvironment);
        }

        [HttpPut]
        public async Task<ActionResult> PutEnvironments(int id, [FromBody] Models.Environment environment)
        {
            if(id != environment.Id)
            {
                return BadRequest();
            }

            await this._environmentRepository.Update(environment);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete (int id)
        {
            var environmentToDelete = await this._environmentRepository.Get(id);

            if (environmentToDelete == null)
                return NotFound("Environment does not exist.");
            
            await this._environmentRepository.Delete(environmentToDelete.Id);
            return NoContent();
        }

    }
}
