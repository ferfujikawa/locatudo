using Locatudo.Compartilhado.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Dominio.Executores
{
    public class ExecutorCancelarLocacao : IExecutor<ComandoCancelarLocacao>
    {
        private readonly IRepositorioLocacao _repositorioLocacao;

        public ExecutorCancelarLocacao(IRepositorioLocacao repositorioLocacao)
        {
            _repositorioLocacao = repositorioLocacao;
        }

        public void Executar(ComandoCancelarLocacao comando)
        {
            var locacao = _repositorioLocacao.ObterPorId(comando.IdLocacao);
            if (locacao == null)
                throw new Exception("Locação não encontrada.");

            if (locacao.Cancelar() == false)
                throw new Exception("A situação atual da locação não permite cancelamento.");
        }
    }
}
