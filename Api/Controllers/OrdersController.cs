using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.OrderCommands;
using Application.DataTransfer;
using Application.Queries.OrderQueries;
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
    public class OrdersController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public OrdersController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // GET: api/Orders
        [HttpGet]
        public IActionResult Get([FromQuery] SearchOrderDto search, [FromServices] IGetOrdersQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, search));
        }

        // GET: api/Orders/5
        [HttpGet("{id}", Name = "Get")]
        public IActionResult Get(int id, [FromServices] IGetOrderQuery query)
        {
            return Ok(_executor.ExecuteQuery(query, id));
        }

        // POST: api/Orders
        [HttpPost]
        public IActionResult Post([FromBody] OrderDto dto, [FromServices] ICreateOrderCommand command)
        {
            _executor.ExecuteCommand(command, dto);
            return StatusCode(StatusCodes.Status201Created);
        }

        // PUT: api/Orders/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] ChangeOrderStatusDto dto, [FromServices] IUpdateOrderCommand command)
        {
            dto.OrderId = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }

    }
}
