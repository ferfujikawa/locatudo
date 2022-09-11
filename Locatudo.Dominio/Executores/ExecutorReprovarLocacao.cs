using Locatudo.Compartilhado.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Dominio.Executores
{
    public class ExecutorReprovarLocacao : IExecutor<ComandoReprovarLocacao>
    {
        private readonly IRepositorioLocacao _repositorioLocacao;
        private readonly IRepositorioFuncionario _repositorioFuncionario;

        public ExecutorReprovarLocacao(
            IRepositorioLocacao repositorioLocacao,
            IRepositorioFuncionario repositorioFuncionario)
        {
            _repositorioLocacao = repositorioLocacao;
            _repositorioFuncionario = repositorioFuncionario;
        }

        public void Executar(ComandoReprovarLocacao comando)
        {
            var aprovador = _repositorioFuncionario.ObterPorId(comando.IdAprovador);
            if (aprovador == null)
                throw new Exception("Funcionário não encontrado.");

            var locacao = _repositorioLocacao.ObterPorId(comando.IdLocacao);
            if (locacao == null)
                throw new Exception("Locação não encontrada.");

            if (locacao.PodeSerAprovadaReprovadaPor(aprovador) == false)
                throw new Exception("Aprovador não está lotado no departamento gerenciador do equipamento.");

            if (locacao.Reprovar(aprovador) == false)
                throw new Exception("A situação atual da locação não permite reprovação.");
        }
    }
}
