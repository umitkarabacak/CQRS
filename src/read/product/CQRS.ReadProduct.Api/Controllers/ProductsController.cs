using CQRS.ReadProduct.Application.Products.Queries.GetProductsListWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.ReadProduct.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly ILogger<ProductsController> _logger;
        private readonly ISender _mediator;

        public ProductsController(ILogger<ProductsController> logger
            , ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var getProductListQuery = new GetProductListQuery(pageSize, pageIndex);

            var products = await _mediator.Send(getProductListQuery);

            return Ok(products);
        }
    }
}