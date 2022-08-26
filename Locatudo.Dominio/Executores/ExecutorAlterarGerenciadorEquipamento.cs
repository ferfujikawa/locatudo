using Locatudo.Compartilhado.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Dominio.Executores
{
    public class ExecutorAlterarGerenciadorEquipamento : IExecutor<ComandoAlterarGerenciadorEquipamento>
    {
        private readonly IRepositorioEquipamento _repositorioEquipamento;
        private readonly IRepositorioDepartamento _repositorioDepartamento;

        public ExecutorAlterarGerenciadorEquipamento(
            IRepositorioEquipamento repositorioEquipamento,
            IRepositorioDepartamento repositorioDepartamento)
        {
            _repositorioEquipamento = repositorioEquipamento;
            _repositorioDepartamento = repositorioDepartamento;
        }

        public void Executar(ComandoAlterarGerenciadorEquipamento comando)
        {
            var equipamento = _repositorioEquipamento.ObterPorId(comando.Id);
            if (equipamento == null)
                throw new Exception("Equipamento não encontrado");

            var departamento = _repositorioDepartamento.ObterPorId(comando.IdDepartamento);
            if (departamento == null)
                throw new Exception("Departamento não encontrado");

            equipamento.AlterarGerenciador(departamento);
            _repositorioEquipamento.Alterar(equipamento);
        }
    }
}
