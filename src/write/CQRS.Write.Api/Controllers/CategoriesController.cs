using CQRS.Write.Application.Categories.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Write.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly IMediator _mediator;

        public CategoriesController(ILogger<CategoriesController> logger
            , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CategoryCreateRequest categoryCreateRequest)
        {
            var response = await _mediator.Send(categoryCreateRequest);

            return Ok(response);
        }
    }
}