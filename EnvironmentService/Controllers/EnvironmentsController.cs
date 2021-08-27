using EnvironmentService.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
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
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IEnumerable<Models.Environment>> GetEnvironments()
        {
            return await this._environmentRepository.Get();
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Models.Environment>> GetEnvironments(int id)
        {
            var environment = await this._environmentRepository.Get(id);

            if (environment == null)
                return NotFound("Not found!");

            return environment;
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<Models.Environment>> PostEnvironments([FromBody] Models.Environment environment)
        {
            var newEnvironment = await this._environmentRepository.Create(environment);
            return CreatedAtAction(nameof(GetEnvironments), new { id = newEnvironment.Id }, newEnvironment);
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
