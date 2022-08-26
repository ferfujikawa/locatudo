using Locatudo.Compartilhado.ObjetosDeValor;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Testes.Repositorios
{
    public class RepositorioFuncionarioFalso : IRepositorioFuncionario
    {
        private readonly Guid _idValido;

        public RepositorioFuncionarioFalso(Guid idValido)
        {
            _idValido = idValido;
        }
        public void Alterar(Funcionario entidade)
        {
            
        }

        public void Criar(Funcionario entidade)
        {
            
        }

        public void Excluir(Guid id)
        {
            
        }

        public IEnumerable<Funcionario> Listar()
        {
            return new List<Funcionario>();
        }

        public Funcionario? ObterPorId(Guid id)
        {
            if (id != _idValido)
                return null;

            var departamento = new Departamento("Departamento 1", new Email("departamento1@provedor.com"));

            return new Funcionario(
                new NomePessoaFisica("Funcionário", "de Tal"),
                new Email("fulanodetal@provedor.com"),
                departamento);
        }
    }
}
