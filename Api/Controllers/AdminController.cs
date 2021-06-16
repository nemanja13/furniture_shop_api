using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using Application.Commands.UserCommands;
using Application.DataTransfer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly UseCaseExecutor _executor;

        public AdminController(UseCaseExecutor executor)
        {
            _executor = executor;
        }

        // PUT: api/Admin/5
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserDto dto, [FromServices] IUpdateUserAdminCommand command)
        {
            dto.Id = id;
            _executor.ExecuteCommand(command, dto);
            return NoContent();
        }
    }
}
