using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.ColorCommands;
using Application.DataTransfer;
using Application.Queries.ColorQueries;
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
    public class ColorsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public ColorsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/Colors
        [HttpGet]
        public IActionResult Get([FromQuery] SearchColorDto search, [FromServices] IGetColorsQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET: api/Colors/5
        [HttpGet("{id}", Name = "GetColor")]
        public IActionResult Get(int id, [FromServices] IGetColorQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST: api/Colors
        [HttpPost]
        public IActionResult Post([FromBody] ColorDto dto, [FromServices] ICreateColorCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Colors/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ColorDto dto, [FromServices] IUpdateColorCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteColorCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
