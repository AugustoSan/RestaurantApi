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

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IMediator mediator, ILogger<AuthController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<AuthController> _logger = logger;


        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginCommand command)
        {
            // 1. Datos de la Conexión
            var remoteIp = HttpContext.Connection.RemoteIpAddress?.ToString();
            var localIp = HttpContext.Connection.LocalIpAddress?.ToString();
            var hostName = HttpContext.Request.Host.Value;
            
            // 2. Datos del Dispositivo (a través del User-Agent)
            var userAgent = HttpContext.Request.Headers["User-Agent"].ToString();

            _logger.LogInformation("Solicitud recibida por el usuario {Username}. IP Cliente: {RemoteIp}, Host: {HostName}, User-Agent: {UserAgent}, IP Servidor: {LocalIp}",
                command.Username, remoteIp, hostName, userAgent, localIp);
                
            return Ok(await _mediator.Send(command));
        }
        [HttpPost("refresh-token")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> RefreshToken([FromBody] string refreshToken)
        {
            var command = new RefreshTokenCommand(refreshToken){
                UserId = User.Identity?.Name ?? ""
            };
            return Ok(await _mediator.Send(command));
        }
    }
}