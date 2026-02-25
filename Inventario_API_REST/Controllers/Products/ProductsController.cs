using Inventario_API_REST.Features.Products;
namespace Inventario_API_REST.Controllers.Products
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class ProductsController(IMediatR _meadiatR) : ControllerBase
    {
        [HttpPost]
        [Authorize(Policy = Permissions.CREATE)]
        public Task<IActionResult> CreateProduct([FromBody] CreateUpdateBodyDto request) =>
         _meadiatR.Send(new CreateProductCommand(
                request.Name,
                request.Description,
                request.Stock,
                request.Cost,
                request.Price,
                User.GetUserId())).ToActionResult();


        [HttpGet]
        [Authorize(Policy = Permissions.READ)]
        public Task<IActionResult> GetAll([FromQuery] int page = 1, [FromQuery] int size = 10) =>
             _meadiatR.Send(new GetProductsQuery(page, size)).ToActionResult();


        [HttpPut("{Id}")]
        [Authorize(Policy = Permissions.UPDATE)]
        public Task<IActionResult> UpdateProduct([FromRoute] int Id, [FromBody] CreateUpdateBodyDto request) =>
             _meadiatR.Send(new UpdateProductCommand(
                Id,
                request.Name,
                request.Description,
                request.Stock,
                request.Cost,
                request.Price)).ToActionResult();

        [HttpDelete("{Id}")]
        [Authorize(Policy = Permissions.DELETE)]
        public Task<IActionResult> DeleteProduct([FromRoute] int Id) =>
             _meadiatR.Send(new DeleteProductCommand(Id)).ToActionResult();
    }

    public record CreateUpdateBodyDto(string Name, string Description, int Stock, decimal Cost, decimal Price);
}
