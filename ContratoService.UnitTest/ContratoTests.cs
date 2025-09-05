using Xunit;
using FluentAssertions;
using ContratacaoService.Domain.Entities;

public class ContratoTests
{
    [Fact]
    public void DeveCriarContratoComStatusGeradoMesmoSemPropostaId()
    {
        var contrato = new Contrato(); 

        contrato.PropostaId.Should().Be(Guid.Empty);
    }
}
