using Inventario_API_REST.Features.Roles;
namespace Inventario_API_REST.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class RoleController(IMediatR _mediatR) : ControllerBase
    {
        [HttpGet]
        public Task<IActionResult> GetRoles() =>
             _mediatR.Send(new GetRolesQuery()).ToActionResult();

    }
}
