using JwtAuthenticationManger.Model;
using JwtAuthenticationManger.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuthenticationManger.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthMangerController : ControllerBase
    {
        private readonly JwtManger _jwtManger;

        public AuthMangerController(JwtManger jwtManger)
        {
            _jwtManger = jwtManger;
        }

        [HttpPost]
        public IActionResult Account ([FromBody] LoginModel model)
        {
            var account = _jwtManger.Authenticate(model);

            return account is null ? Unauthorized() : Ok(account);
        }
    }
}
