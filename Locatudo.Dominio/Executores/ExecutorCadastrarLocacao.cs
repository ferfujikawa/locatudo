using Locatudo.Compartilhado.Executores;
using Locatudo.Compartilhado.ObjetosDeValor;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Dominio.Executores
{
    public class ExecutorCadastrarLocacao : IExecutor<ComandoCadastrarLocacao>
    {
        private readonly IRepositorioEquipamento _repositorioEquipamento;
        private readonly IRepositorioUsuario _repositorioUsuario;
        private readonly IRepositorioLocacao _repositorioLocacao;

        public ExecutorCadastrarLocacao(
            IRepositorioEquipamento repositorioEquipamento,
            IRepositorioUsuario repositorioUsuario,
            IRepositorioLocacao repositorioLocacao)
        {
            _repositorioEquipamento = repositorioEquipamento;
            _repositorioUsuario = repositorioUsuario;
            _repositorioLocacao = repositorioLocacao;
        }

        public void Executar(ComandoCadastrarLocacao comando)
        {
            var equipamento = _repositorioEquipamento.ObterPorId(comando.IdEquipamento);
            var horarioLocacao = new HorarioLocacao(comando.Inicio);
            if (equipamento == null)
                throw new Exception("Equipamento não encontrado");

            var locatario = _repositorioUsuario.ObterPorId(comando.IdLocatario);
            if (locatario == null)
                throw new Exception("Usuário não encontrado");

            if (horarioLocacao.Inicio < DateTime.Now)
                throw new Exception("Início não pode ser no passado");

            if (_repositorioLocacao.VerificarDisponibilidade(comando.IdEquipamento, horarioLocacao) == false)
                throw new Exception("Horário de locação indisponível");

            var locacao = new Locacao(equipamento, locatario, horarioLocacao);
            _repositorioLocacao.Criar(locacao);
        }
    }
}
