using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.ObjetosDeValor;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Testes.Repositorios
{
    public class RepositorioDepartamentoFalso : IRepositorioDepartamento
    {
        public void Alterar(Departamento entidade)
        {
            
        }

        public void Criar(Departamento entidade)
        {
            
        }

        public void Excluir(Guid id)
        {
            
        }

        public IEnumerable<Departamento> Listar()
        {
            return new List<Departamento>();
        }

        public Departamento? ObterPorId(Guid id)
        {
            var email = new Email("departamento.teste@provedor.com");
            return new Departamento("Departamento Teste", email);
        }
    }
}
