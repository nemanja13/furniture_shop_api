using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.CommentCommands;
using Application.DataTransfer;
using Application.Queries.CommentQueries;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public CommentsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }


        // GET: api/Comments/5
        [HttpGet("{id}", Name = "GetComment")]
        public IActionResult Get(int id, [FromServices] IGetCommentQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST: api/Comments
        [HttpPost]
        public IActionResult Post([FromBody] CommentDto dto, [FromServices] ICreateCommentCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Comments/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] CommentDto dto, [FromServices] IUpdateCommentCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id, [FromServices] IDeleteCommentCommand command)
        {
            _executor.ExecuteCommand(command, id);
            return NoContent();
        }
    }
}
