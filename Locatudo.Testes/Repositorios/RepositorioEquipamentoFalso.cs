using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Testes.Repositorios
{
    public class RepositorioEquipamentoFalso : IRepositorioEquipamento
    {
        public void Alterar(Equipamento entidade)
        {
            
        }

        public void Criar(Equipamento entidade)
        {
            
        }

        public void Excluir(Guid id)
        {
            
        }

        public IEnumerable<Equipamento> Listar()
        {
            return new List<Equipamento>();
        }

        public Equipamento? ObterPorId(Guid id)
        {
            return new Equipamento("Equipamento teste");
        }
    }
}
