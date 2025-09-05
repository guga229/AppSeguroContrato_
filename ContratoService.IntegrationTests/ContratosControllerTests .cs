using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Text;
using System.Text.Json;

namespace ContratoService.IntegrationTests
{
    public class ContratosControllerTests : IClassFixture<WebApplicationFactory<Program>>
    {
        private readonly HttpClient _client;
        public ContratosControllerTests(WebApplicationFactory<Program> factory)
        {
            _client = factory.CreateClient();
        }

        [Fact]
        public async Task Post_Contrato_DeveRetornar201_QuandoDadosValidos()
        {
            // Arrange
            var propostaId = Guid.NewGuid();
            var content = new StringContent(JsonSerializer.Serialize(new { propostaId }), Encoding.UTF8, "application/json");

            // Act
            var response = await _client.PostAsync("/api/contratos", content);

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.Created);
        }

        [Fact]
        public async Task Get_Contratos_DeveRetornarListaDeContratos()
        {
            // Act
            var response = await _client.GetAsync("/api/contratos");

            // Assert
            response.StatusCode.Should().Be(System.Net.HttpStatusCode.OK);
            var body = await response.Content.ReadAsStringAsync();
            body.Should().Contain("propostaId"); // ou outro campo esperado do contrato
        }
    }
}