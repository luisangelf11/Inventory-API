using Inventario_API_REST.Features.Permissions;
namespace Inventario_API_REST.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController(IMediatR mediatR) : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetPermissions(CancellationToken cancellationToken = default) =>
             mediatR.Send(new GetPermissionsQuery(), cancellationToken).ToActionResult();
    }
}
