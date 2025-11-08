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
using Restaurant.Api.Application.Establishment.Queries.GetInfoEstablishment;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EstablishmentController(IMediator mediator, ILogger<EstablishmentController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<EstablishmentController> _logger = logger;


        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetInfoEstablishmentQuery()));
        }
        // [HttpPatch()]
        // [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        // public async Task<IActionResult> Update([FromBody] UpdateCommand command)
        // {
        //     return Ok(await _mediator.Send(command));
        // }
    }
}