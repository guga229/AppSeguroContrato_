namespace ContratacaoService.Api.Queue
{
    public class FilaSimples
    {
        private readonly Queue<string> _fila = new();

        public void Enfileirar(string mensagem)
        {
            _fila.Enqueue(mensagem);
        }

        public string? Desenfileirar()
        {
            if (_fila.Count == 0) return null;
            return _fila.Dequeue();
        }

        public int Tamanho => _fila.Count;
    }

}
