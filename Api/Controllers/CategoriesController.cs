using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.CategoryCommands;
using Application.DataTransfer;
using Application.Queries.CategoryQueries;
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
    public class CategoriesController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public CategoriesController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/Categories
        [HttpGet]
        public IActionResult Get([FromQuery] SearchCategoryDto search, [FromServices] IGetCategoriesQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET: api/Categories/5
        [HttpGet("{id}", Name = "GetCategory")]
        public IActionResult Get(int id, [FromServices] IGetCategoryQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST: api/Categories
        [HttpPost]
        public IActionResult Post([FromBody] CategoryDto dto, [FromServices] ICreateCategoryCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Categories/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CategoryDto dto, [FromServices] IUpdateCategoryCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCategoryCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
