using Locatudo.Compartilhado.Executores.Comandos;

namespace Locatudo.Dominio.Executores.Comandos
{
    public class ComandoReprovarLocacao : IComandoExecutor
    {
        public ComandoReprovarLocacao()
        {
        }

        public ComandoReprovarLocacao(Guid idLocacao, Guid idAprovador)
        {
            IdLocacao = idLocacao;
            IdAprovador = idAprovador;
        }

        public Guid IdLocacao { get; set; }
        public Guid IdAprovador { get; set; }
    }
}
