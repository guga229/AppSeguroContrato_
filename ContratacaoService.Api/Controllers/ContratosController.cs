using ContratacaoService.Api.Queue;
using ContratacaoService.Application.Services;
using ContratacaoService.Domain;
using ContratacaoService.Domain.Entities;
using ContratacaoService.Infrastructure;
using ContratacaoService.Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;

namespace ContratacaoService.Api.Controllers
{
    [ApiController]
    [Route("api/contratos")]
    public class ContratosController : ControllerBase
    {
        private readonly ContratacaoDbContext _context;
        private readonly PropostaClient _propostaClient;
        private readonly FilaSimples _fila;

        public ContratosController(
            ContratacaoDbContext context,
            PropostaClient propostaClient,
            FilaSimples fila)
        {
            _context = context;
            _propostaClient = propostaClient;
            _fila = fila;
        }

        [HttpPost]
        public async Task<IActionResult> Contratar([FromBody] Guid propostaId)
        {
            var status = await _propostaClient.ObterStatusProposta(propostaId);

            if (status != PropostaStatus.Aprovada)
                return BadRequest("Proposta não está aprovada.");

            var contrato = new Contrato
            {
                Id = Guid.NewGuid(),
                PropostaId = propostaId,
                DataContratacao = DateTime.UtcNow
            };

            _context.Contratos.Add(contrato);
            await _context.SaveChangesAsync();

            _fila.Enfileirar($"Contrato gerado para proposta {propostaId}");

            return CreatedAtAction(nameof(Contratar), new { id = contrato.Id }, contrato);
        }

        [HttpGet]
        public IActionResult ListarContratos()
        {
            var contratos = _context.Contratos.ToList();
            return Ok(contratos);
        }
    }
}
