using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Restaurant.Api.Application.Categories.Queries.GetAllCategories;

namespace Restaurant.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CategoryController(IMediator mediator, ILogger<CategoryController> logger) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;
        private readonly ILogger<CategoryController> _logger = logger;


        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return Ok(await _mediator.Send(new GetAllCategoriesQuery()));
        }
    }
}