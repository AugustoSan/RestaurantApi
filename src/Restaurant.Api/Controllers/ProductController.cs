using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Api.Application.Products.Queries.GetAllProductsQuery;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProductController(IMediator mediator, ILogger<ProductController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<ProductController> _logger = logger;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _mediator.Send(new GetAllProductsQuery()));
        }
    }
}