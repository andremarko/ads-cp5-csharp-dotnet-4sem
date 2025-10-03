using Microsoft.AspNetCore.Mvc;
using ApiEventosCulturais.Services;

namespace ApiEventosCulturais.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AuthService _authService;
        public AuthController(AuthService authService)
        {
            _authService = authService;
        }

        // Login por username e senha (cadastrados no banco)
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string username, [FromForm] string senha)
        {
            var token = await _authService.Login(username, senha);

            if (token == null)
                return Unauthorized(new { message = "Usuário ou senha inválidos" });

            return Ok(new { token });
        }
    }
}
