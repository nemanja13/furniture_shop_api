using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.MaterialCommands;
using Application.DataTransfer;
using Application.Queries.MaterialQueries;
using Application.Searches;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MaterialsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public MaterialsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/Materials
        [HttpGet]
        public IActionResult Get([FromQuery] SearchMaterialDto search, [FromServices] IGetMaterialsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET: api/Materials/5
        [HttpGet("{id}", Name = "GetMaterial")]
        public IActionResult Get(int id, [FromServices] IGetMaterialQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST: api/Materials
        [HttpPost]
        public IActionResult Post([FromBody] MaterialDto dto, [FromServices] ICreateMaterialCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Materials/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] MaterialDto dto, [FromServices] IUpdateMaterialCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteMaterialCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
