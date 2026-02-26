using Inventario_API_REST.Features.Auth;
namespace Inventario_API_REST.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IMediatR mediatR) : ControllerBase
    {
        [HttpPost("Login")]
        [AllowAnonymous]
        public Task<IActionResult> Login([FromBody] AuthLoginCommand request, CancellationToken cancellationToken = default) =>
            mediatR.Send(request, cancellationToken).ToActionResult();


        [HttpPost("Register")]
        [Authorize(Policy = RolesName.Admin)]
        public Task<IActionResult> Register([FromBody] AuthRegisterCommand request, CancellationToken cancellationToken = default) =>
             mediatR.Send(request, cancellationToken).ToActionResult();

    }
}