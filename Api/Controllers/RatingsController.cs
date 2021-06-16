using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.RatingCommands;
using Application.DataTransfer;
using Application.Queries.RatingQueries;
using DataAccess;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RatingsController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public RatingsController(UseCaseExecutor executor)
        {
            _executor = executor;
        }


        // GET: api/Ratings/5
        [HttpGet("{id}", Name = "GetRating")]
        public IActionResult Get(int id, [FromServices] IGetRatingQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST: api/Ratings
        [HttpPost]
        public IActionResult Post([FromBody] RatingDto dto, [FromServices] ICreateRatingCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Ratings/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] RatingDto dto, [FromServices] IUpdateRatingCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

    }
}
