using Inventario_API_REST.Features.Roles;
namespace Inventario_API_REST.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController(IMediatR mediatR) : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetRoles(CancellationToken cancellationToken = default) =>
             mediatR.Send(new GetRolesQuery(), cancellationToken).ToActionResult();
    }
}
