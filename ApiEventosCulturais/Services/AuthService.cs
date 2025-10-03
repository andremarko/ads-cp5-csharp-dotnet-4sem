using ApiEventosCulturais.Model;
using ApiEventosCulturais.Models;
using BCryptNet = BCrypt.Net.BCrypt;

using MongoDB.Driver;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace ApiEventosCulturais.Services
{
    public class AuthService
    {

        private readonly IMongoCollection<Usuario> _usuarioCollection;
        private readonly IConfiguration _config;

        public AuthService(IMongoCollection<Usuario> usuarioCollection, IConfiguration config)
        {
            _usuarioCollection = usuarioCollection;
            _config = config;
        }


        // Busca usuário com username. Caso não exista, retorna null
        public async Task<string> Login(string username, string senha)
        {
            var user = await _usuarioCollection.Find(u => u.Username == username).FirstOrDefaultAsync();
            
            if (user == null || !BCryptNet.Verify(senha, user.Senha))
                return null;

            // retorna token de autenticação
            return GenerateToken(user);

        }

        // Gera token de autenticação
        public string GenerateToken(Usuario usuario)
        {
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);
            var creds = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario ?? ""),
                    new Claim(ClaimTypes.Name, usuario.Username),
                    new Claim(ClaimTypes.Role, usuario.Role ?? "User")
                },
                expires: DateTime.UtcNow.AddMinutes(_config.GetValue<int>("Jwt:ExpiresMinutes")),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
