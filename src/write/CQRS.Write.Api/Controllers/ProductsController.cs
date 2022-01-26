using CQRS.Write.Application.Products.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.Write.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly IMediator _mediator;

        public ProductsController(ILogger<ProductsController> logger
            , IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] ProductCreateRequest productCreateRequest)
        {
            var response = await _mediator.Send(productCreateRequest);

            return Ok(response);
        }
    }
}
