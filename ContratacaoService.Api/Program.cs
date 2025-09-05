using ContratacaoService.Api.Queue;
using ContratacaoService.Api.Workers;
using ContratacaoService.Application.Services;
using ContratacaoService.Infrastructure;
using ContratacaoService.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");


builder.Services.AddDbContext<ContratacaoDbContext>(options =>
    options.UseNpgsql(connectionString));


builder.Services.AddHttpClient<PropostaClient>(client =>
{
    client.BaseAddress = new Uri("https://localhost:44340"); 
})
.ConfigurePrimaryHttpMessageHandler(() =>
{
    return new HttpClientHandler
    {
        ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
    };
});


builder.Services.AddControllers();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<FilaSimples>();
builder.Services.AddHostedService<FilaWorker>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
