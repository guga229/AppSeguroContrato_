
```markdown
# 📄 ContratacaoService

O **ContratacaoService** é um microserviço desenvolvido para gerenciar a contratação de propostas aprovadas no sistema AppSeguros. Ele garante que apenas propostas com status **Aprovada** sejam contratadas, validando essa informação diretamente com o PropostaService.

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

```
ContratacaoService
├── Api           → Camada de apresentação (controllers)
├── Application   → Regras de negócio e integração com PropostaService
├── Infrastructure→ Persistência e fila de mensagens
├── Domain        → Entidades e enums do domínio
```

---

## 📡 Comunicação com PropostaService

Antes de efetivar a contratação, o serviço realiza uma requisição HTTP para validar o status da proposta:

```http
GET https://localhost:44340/api/propostas/{id}
```

A contratação só prossegue se o status retornado for **Aprovada**.

---

## 🗄️ Banco de Dados

- **Banco**: `contratacao_db`
- **Tabela principal**: `Contratos`
- **Migrations**: gerenciadas via EF Core

### Criar Migration Inicial

```bash
dotnet ef migrations add InitialCreate --project ContratacaoService.Infrastructure --startup-project ContratacaoService.Api
dotnet ef database update --project ContratacaoService.Infrastructure --startup-project ContratacaoService.Api
```

---

## 📬 Fila de Mensagens

O serviço utiliza uma fila simples em memória (`FilaSimples`) para processar mensagens assíncronas, como notificações e envio de e-mails:

```csharp
_fila.Enfileirar($"Contrato gerado para proposta {propostaId}");
```

---

## 📡 Endpoints Principais

| Método | Rota            | Descrição                         |
|--------|------------------|-----------------------------------|
| POST   | /api/contratos   | Contrata uma proposta aprovada   |
| GET    | /api/contratos   | Lista todos os contratos gerados |

---

## 🧪 Testes

Os testes estão organizados nas seguintes pastas:

- `/tests/ContratacaoService.UnitTests`
- `/tests/ContratacaoService.IntegrationTests`

### Executar Testes

```bash
dotnet test
```

---

## ⚙️ Configuração Local

1. **PostgreSQL**  
   Certifique-se de que o PostgreSQL está rodando localmente na porta `5432` com o banco `contratacao_db` criado.

2. **Connection String**  
   Configure o `appsettings.json` com a string de conexão:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Port=5432;Database=contratacao_db;Username=postgres;Password=postgres"
}
```

---

## 📌 Observações

- O uso de RabbitMQ é opcional e pode ser substituído pela fila em memória para ambientes de desenvolvimento.
- O serviço está preparado para evoluir com integrações mais robustas e escaláveis.

---

## 🛠️ Contribuições

Contribuições são bem-vindas! Sinta-se à vontade para abrir issues ou enviar pull requests com melhorias, correções ou novas funcionalidades.

---

