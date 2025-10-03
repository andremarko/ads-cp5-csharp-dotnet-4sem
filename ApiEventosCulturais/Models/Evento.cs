using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace ApiEventosCulturais.Model
{
    public class Evento
    {

        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("id_evento")]
        public string? IdEvento { get; set; }

        public string Titulo { get; set; }

        
        public string Descricao { get; set; }
        
        
        public DateTime Data { get; set; }
        
        
        public string Local { get; set; }
        
        
        public string Categoria { get; set; }
        
        
        public int Capacidade { get; set; }


        [BsonElement("data_criacao")]
        public DateTime DataCriacao { get; set; }
    }
}
