using ContratacaoService.Api.Queue;

namespace ContratacaoService.Api.Workers
{
    public class FilaWorker : BackgroundService
    {
        private readonly FilaSimples _fila;

        public FilaWorker(FilaSimples fila)
        {
            _fila = fila;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var mensagem = _fila.Desenfileirar();
                if (mensagem != null)
                {
                    Console.WriteLine($"[Fila] Processando: {mensagem}");
                
                }

                await Task.Delay(1000); 
            }
        }
    }

}
