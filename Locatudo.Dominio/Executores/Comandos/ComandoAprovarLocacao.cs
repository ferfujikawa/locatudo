using Locatudo.Compartilhado.Executores.Comandos;

namespace Locatudo.Dominio.Executores.Comandos
{
    public class ComandoAprovarLocacao : IComandoExecutor
    {
        public ComandoAprovarLocacao()
        {
        }

        public ComandoAprovarLocacao(Guid idLocacao, Guid idAprovador)
        {
            IdLocacao = idLocacao;
            IdAprovador = idAprovador;
        }

        public Guid IdLocacao { get; set; }
        public Guid IdAprovador { get; set; }
    }
}
