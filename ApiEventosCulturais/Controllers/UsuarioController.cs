using Microsoft.AspNetCore.Mvc;
using ApiEventosCulturais.Models;
using MongoDB.Driver;
using BCryptNet = BCrypt.Net.BCrypt;

namespace ApiEventosCulturais.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IMongoCollection<Usuario> _usuarioCollection;

        public UsuarioController(IMongoCollection<Usuario> usuarioCollection)
        {
            _usuarioCollection = usuarioCollection;
        }

        // Cadastra usuário. Por padrão, cria um usuário comum
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Usuario novoUsuario)
        {
            novoUsuario.IdUsuario = null;
            novoUsuario.Senha = BCryptNet.HashPassword(novoUsuario.Senha);

            await _usuarioCollection.InsertOneAsync(novoUsuario);
            return Ok(new { message = "Usuário registrado com sucesso!" });
        }
    }
}
