using Inventario_API_REST.Features.Products;

namespace Inventario_API_REST.Controllers.Products
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IMediatR _meadiatR) : BaseController
    {
        [HttpPost]
        [Authorize(Policy = Permissions.CREATE)]
        public async Task<IActionResult> CreateProduct([FromBody] CreateUpdateBodyDto request)
        {
            var userId = User.GetUserId();

            var result = await _meadiatR.Send(new CreateProductCommand(
                request.Name,
                request.Description,
                request.Stock,
                request.Cost,
                request.Price,
                userId));

            return HandleResult(result);
        }

        [HttpGet]
        [Authorize(Policy = Permissions.READ)]
        public async Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10)
        {
            var result = await _meadiatR.Send(new GetProductsQuery(page, size));
            return HandleResult(result);
        }

        [HttpPut("{Id}")]
        [Authorize(Policy = Permissions.UPDATE)]
        public async Task<IActionResult> UpdateProduct([FromRoute] int Id, [FromBody] CreateUpdateBodyDto request)
        {
            var result = await _meadiatR.Send(new UpdateProductCommand(
                Id,
                request.Name,
                request.Description,
                request.Stock,
                request.Cost,
                request.Price));

            return HandleResult(result);
        }

        [HttpDelete("{Id}")]
        [Authorize(Policy = Permissions.DELETE)]
        public async Task<IActionResult> DeleteProduct([FromRoute] int Id)
        {
            var result = await _meadiatR.Send(new DeleteProductCommand(Id));
            return HandleResult(result);
        }

    }

    public record CreateUpdateBodyDto(string Name, string Description, int Stock, decimal Cost, decimal Price);
}
