using Inventario_API_REST.Features.Permissions;

namespace Inventario_API_REST.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class PermissionController(IMediatR _mediatR) : BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetPermissions()
        {
            var result = await _mediatR.Send(new GetPermissionsQuery());
            return HandleResult(result);
        }
    }
}
