using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ApiEventosCulturais.Model;
using ApiEventosCulturais.Services;

namespace ApiEventosCulturais.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    // Requer autenticação para realizar operações nesse endpoint
    [Authorize]
    public class EventoController : ControllerBase
    {
        private readonly EventoService _eventoService;

        // Utiliza o EventoService como lógica de negócio
        public EventoController(EventoService eventoService)
        {
            _eventoService = eventoService;
        }

        // Retorna todos os eventos
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var eventos = await _eventoService.GetAllAsync();
            return Ok(eventos);
        }

        // Retorna um evento em específico 
        [HttpGet("{idEvento}")]
        public async Task<IActionResult> GetById(string idEvento)
        {
            var evento = await _eventoService.GetByIdAsync(idEvento);
            if (evento == null) return NotFound(new {mensagem = "Evento não encontrado"});
            return Ok(evento);  
        }

        // Cria um evento
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] Evento evento)
        {
            var criado = await _eventoService.CreateAsync(evento);
            return CreatedAtAction(nameof(GetById), new { idEvento = criado.IdEvento }, criado);
        }

        // Atualiza um evento
        [HttpPut("{idEvento}")]
        public async Task<IActionResult> Update(string idEvento, [FromBody] Evento updatedEvento)
        {
            var ok = await _eventoService.UpdateAsync(idEvento, updatedEvento);
            if (!ok) return NotFound(new {mensagem = "Evento não encontrado"});
            return NoContent();
        }

        // Deleta um evento
        [HttpDelete("{idEvento}")]
        public async Task<IActionResult> Delete(string idEvento)
        {
            var ok = await _eventoService.DeleteAsync(idEvento);
            if (!ok) return NotFound(new {mensagem = "Evento não encontrado"});
            return NoContent();
        }
    }
}
