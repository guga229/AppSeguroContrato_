using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace ContratacaoService.Application.Services
{
    public class PropostaClient
    {
        private readonly HttpClient _http;

        public PropostaClient(HttpClient http)
        {
            _http = http;
        }

        public async Task<PropostaStatus?> ObterStatusProposta(Guid propostaId)
        {
            var response =  await _http.GetAsync($"https://localhost:44340/api/propostas/{propostaId}");
            response.EnsureSuccessStatusCode();

            var proposta = await response.Content.ReadFromJsonAsync<PropostaDto>();
            return proposta?.Status;
        }
    }

    public class PropostaDto
    {
        public Guid Id { get; set; }
        public PropostaStatus Status { get; set; }
    }

    public enum PropostaStatus
    {
        EmAnalise = 0,
        Aprovada = 1,
        Rejeitada = 2
    }
}
