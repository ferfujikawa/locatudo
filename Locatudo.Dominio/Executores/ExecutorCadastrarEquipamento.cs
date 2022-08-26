using Locatudo.Compartilhado.Executores;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Dominio.Executores
{
    public class ExecutorCadastrarEquipamento : IExecutor<ComandoCadastrarEquipamento>
    {
        private readonly IRepositorioEquipamento _repositorioEquipamento;

        public ExecutorCadastrarEquipamento(IRepositorioEquipamento repositorioEquipamento)
        {
            _repositorioEquipamento = repositorioEquipamento;
        }

        public void Executar(ComandoCadastrarEquipamento comando)
        {
            var equipamento = new Equipamento(comando.Nome);
            _repositorioEquipamento.Criar(equipamento);
        }
    }
}
