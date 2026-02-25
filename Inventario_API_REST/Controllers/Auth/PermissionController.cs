using Inventario_API_REST.Features.Permissions;
namespace Inventario_API_REST.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController(IMediatR _mediatR) : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetPermissions() =>
             _mediatR.Send(new GetPermissionsQuery()).ToActionResult();

    }
}
