using Inventario_API_REST.Features.Auth;

namespace Inventario_API_REST.Controllers.Auth
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController(IMediatR _mediatR) : BaseController 
    {
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] AuthLoginCommand request)
        {
            var result = await _mediatR.Send(request);
            return HandleResult(result);
        }

        [HttpPost("Register")]
        [Authorize(Policy = RolesName.Admin)]
        public async Task<IActionResult> Register([FromBody] AuthRegisterCommand request)
        {
            var result = await _mediatR.Send(request);
            return HandleResult(result);
        }
    }
}