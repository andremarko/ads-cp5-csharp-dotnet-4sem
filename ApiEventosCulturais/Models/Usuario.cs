using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiEventosCulturais.Models
{
    public class Usuario
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string? IdUsuario;
        
        public string Username { get; set; }
        
        public string Senha { get; set; }
        public string Role { get; set; } = "Comum";
    }
}
