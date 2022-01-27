using CQRS.ReadCategory.Application.Categories.Queries.GetCategoriesListWithPagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CQRS.ReadCategory.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriesController : ControllerBase
    {
        private readonly ILogger<CategoriesController> _logger;
        private readonly ISender _mediator;

        public CategoriesController(ILogger<CategoriesController> logger
            , ISender mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] int pageSize = 10, [FromQuery] int pageIndex = 0)
        {
            var getCategoryListQuery = new GetCategoryListQuery(pageSize, pageIndex);

            var categories = await _mediator.Send(getCategoryListQuery);

            return Ok(categories);
        }
    }
}
