using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Api.Application.Auth.Commands.Login;
using Restaurant.Api.Application.Auth.Commands.RefreshToken;
using Restaurant.Api.Application.User.Commands.Registry;
using Restaurant.Api.Application.User.Queries.GetAllUsers;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<UserController> _logger = logger;


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllUsersQuery()));
        }
        [HttpPost("registry")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Registry([FromBody] RegistryCommand command)
        {
            return Ok(await _mediator.Send(command));
        }
    }
}