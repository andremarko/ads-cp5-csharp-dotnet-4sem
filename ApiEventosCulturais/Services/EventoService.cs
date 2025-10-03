using ApiEventosCulturais.Model;
using MongoDB.Driver;

namespace ApiEventosCulturais.Services
{
    public class EventoService
    {
        private readonly IMongoCollection<Evento> _eventoCollection;

        public EventoService(IMongoCollection<Evento> eventoCollection)
        {
            _eventoCollection = eventoCollection;
        }

        public async Task<List<Evento>> GetAllAsync() =>
            await _eventoCollection.Find(_ => true).ToListAsync();

        public async Task<Evento?> GetByIdAsync(string idEvento) =>
            await _eventoCollection.Find(a => a.IdEvento == idEvento).FirstOrDefaultAsync();

        public async Task<Evento> CreateAsync(Evento evento)
        {
            evento.IdEvento = null; 
            evento.DataCriacao = DateTime.UtcNow; 
            await _eventoCollection.InsertOneAsync(evento);
            return evento;
        }

        public async Task<bool> UpdateAsync(string idEvento, Evento updatedEvento)
        {
            var existente = await _eventoCollection.Find(a => a.IdEvento == idEvento).FirstOrDefaultAsync();
            if (existente == null) return false;

            updatedEvento.IdEvento = existente.IdEvento;
            updatedEvento.DataCriacao = existente.DataCriacao;

            var result = await _eventoCollection.ReplaceOneAsync(
                a => a.IdEvento == idEvento,
                updatedEvento
            );

            return result.MatchedCount > 0;
        }

        public async Task<bool> DeleteAsync(string idEvento)
        {
            var result = await _eventoCollection.DeleteOneAsync(a => a.IdEvento == idEvento);
            return result.DeletedCount > 0;
        }
    }
}
