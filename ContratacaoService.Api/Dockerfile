# Imagem base do .NET SDK para build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia os arquivos e restaura dependências
COPY . .
RUN dotnet restore ContratacaoService.Api/ContratacaoService.Api.csproj

# Build do projeto
RUN dotnet publish ContratacaoService.Api/ContratacaoService.Api.csproj -c Release -o /app/publish

# Imagem base para runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Porta exposta
EXPOSE 8080

# Comando de inicialização
ENTRYPOINT ["dotnet", "ContratacaoService.Api.dll"]
