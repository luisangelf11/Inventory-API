using Inventario_API_REST.Features.Products;
namespace Inventario_API_REST.Controllers.Products
{
     [Authorize]
     [ApiController]
     [Route("[controller]")]
     public class ProductsController(IMediatR meadiatR) : ControllerBase
     {
          [HttpPost]
          [Authorize(Policy = Permissions.CREATE)]
          public Task<IActionResult> CreateProduct([FromBody] CreateUpdateBodyDto request, CancellationToken cancellationToken = default) =>
           meadiatR.Send(new CreateProductCommand(
                  request.Name,
                  request.Description,
                  request.Stock,
                  request.Cost,
                  request.Price,
                  User.GetUserId()), cancellationToken).ToActionResult();


          [HttpGet]
          [Authorize(Policy = Permissions.READ)]
          public Task<IActionResult> GetAll([FromQuery] ProductsQuery query, CancellationToken cancellationToken = default) =>
               meadiatR.Send(new GetProductsQuery(query.page, query.size), cancellationToken).ToActionResult();


          [HttpPut("{Id}")]
          [Authorize(Policy = Permissions.UPDATE)]
          public Task<IActionResult> UpdateProduct([FromRoute] int Id, [FromBody] CreateUpdateBodyDto request, CancellationToken cancellationToken = default) =>
               meadiatR.Send(new UpdateProductCommand(
                  Id,
                  request.Name,
                  request.Description,
                  request.Stock,
                  request.Cost,
                  request.Price), cancellationToken).ToActionResult();

          [HttpDelete("{Id}")]
          [Authorize(Policy = Permissions.DELETE)]
          public Task<IActionResult> DeleteProduct([FromRoute] int Id, CancellationToken cancellationToken = default) =>
               meadiatR.Send(new DeleteProductCommand(Id), cancellationToken).ToActionResult();
     }

     public record CreateUpdateBodyDto(string Name, string Description, int Stock, decimal Cost, decimal Price);
     public record ProductsQuery(int page = 1, int size = 10);
}
