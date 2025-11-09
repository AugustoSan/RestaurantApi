using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Api.Application.Auth.Commands.Login;
using Restaurant.Api.Application.Auth.Commands.RefreshToken;
using Restaurant.Api.Application.User.Commands.Registry;
using Restaurant.Api.Application.User.Commands.Update;
using Restaurant.Api.Application.User.Queries.GetAllUsers;
using Restaurant.Api.Application.User.Queries.GetUserById;
using Restaurant.Api.Application.User.Commands.ChangePassword;
using Restaurant.Api.Application.User.Commands.ChangeRole;
using Restaurant.Api.Application.User.Commands.Delete;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<UserController> _logger = logger;


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            _logger.LogInformation("Getting all users");
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }
        [HttpGet("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> GetById([FromRoute] string id)
        {
            return Ok(await _mediator.Send(new GetUserByIdQuery(){Id = id}));
        }
        [HttpPost("registry")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Registry([FromBody] RegistryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
        [HttpPatch("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Update([FromRoute] string id, [FromBody] UpdateCommand command)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }
        [HttpPatch("change-password")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordCommand command)
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            command.Id = userId ?? string.Empty;
            return Ok(await _mediator.Send(command));
        }
        [HttpPatch("{id}/change-role")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChangeRole([FromRoute] string id, [FromBody] ChangeRoleCommand command)
        {
            command.Id = id;
            return Ok(await _mediator.Send(command));
        }

        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Delete([FromRoute] string id)
        {
            return Ok(await _mediator.Send(new DeleteCommand(){Id = id}));
        }
    }
}