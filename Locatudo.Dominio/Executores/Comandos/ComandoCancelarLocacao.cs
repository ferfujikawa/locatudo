using Locatudo.Compartilhado.Executores.Comandos;

namespace Locatudo.Dominio.Executores.Comandos
{
    public class ComandoCancelarLocacao : IComandoExecutor
    {
        public ComandoCancelarLocacao()
        {
        }

        public ComandoCancelarLocacao(Guid idLocacao)
        {
            IdLocacao = idLocacao;
        }

        public Guid IdLocacao { get; set; }
    }
}
