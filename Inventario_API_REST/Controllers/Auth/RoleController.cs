using Inventario_API_REST.Features.Roles;

namespace Inventario_API_REST.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController (IMediatR _mediatR): BaseController
    {
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _mediatR.Send(new GetRolesQuery());
            return HandleResult(result);
        }
    }
}
