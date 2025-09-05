# 📄 ContratacaoService

O **ContratacaoService** é um microserviço responsável por gerenciar a contratação de propostas aprovadas no sistema AppSeguros. Ele se comunica com o PropostaService para validar o status da proposta antes de efetivar a contratação.

---

## 🚀 Tecnologias Utilizadas

- [.NET 8](https://dotnet.microsoft.com/)
- [Entity Framework Core](https://learn.microsoft.com/ef/)
- [PostgreSQL](https://www.postgresql.org/)
- ASP.NET Core Web API
- xUnit + FluentAssertions (testes)
- RabbitMQ (opcional para mensageria)
- Fila em memória (implementação leve)

---

## 🧱 Arquitetura
Você colou um conteúdo que tem algumas partes formatadas em Markdown e outras não. Eu vou organizar tudo para que a formatação fique consistente e pronta para usar no seu README.

ContratacaoService
Api: Camada de apresentação (controllers)

Application: Regras de negócio e integração com PropostaService

Infrastructure: Persistência e fila de mensagens

Domain: Entidades e enums do domínio

📡 Comunicação com PropostaService
Antes de contratar uma proposta, o serviço consulta o PropostaService via HTTP para verificar se o status é Aprovada.

HTTP

GET https://localhost:44340/api/propostas/{id}
🗄️ Banco de Dados
Banco: contratacao_db

Tabela principal: Contratos

Migrations: gerenciadas via EF Core

📦 Migration inicial
Bash

dotnet ef migrations add InitialCreate --project ContratacaoService.Infrastructure --startup-project ContratacaoService.Api
dotnet ef database update --project ContratacaoService.Infrastructure --startup-project ContratacaoService.Api
📬 Fila de Mensagens
O serviço possui uma fila simples em memória (FilaSimples) para processar mensagens assíncronas, como notificações ou disparo de e-mails.

C#

_fila.Enfileirar($"Contrato gerado para proposta {propostaId}");
📡 Endpoints Principais
Método	Rota	Descrição
POST	/api/contratos	Contrata uma proposta aprovada
GET	/api/contratos	Lista todos os contratos gerados

Exportar para as Planilhas
🧪 Testes
Os testes estão localizados em:

/tests/ContratacaoService.UnitTests

/tests/ContratacaoService.IntegrationTests

Para rodar os testes:

Bash

dotnet test
⚙️ Configuração Local
1. PostgreSQL
Certifique-se de que o PostgreSQL está rodando localmente na porta 5432 com o banco contratacao_db criado.

2. Connection String
No appsettings.json:

JSON

"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=contratacao_db;Username=p
