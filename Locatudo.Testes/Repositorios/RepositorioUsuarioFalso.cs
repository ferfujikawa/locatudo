using Locatudo.Compartilhado.ObjetosDeValor;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Testes.Repositorios
{
    public class RepositorioUsuarioFalso : IRepositorioUsuario
    {
        private readonly Guid _idValido = Guid.NewGuid();

        public RepositorioUsuarioFalso(Guid idValido)
        {
            _idValido = idValido;
        }
        public void Alterar(Usuario entidade)
        {
            
        }

        public void Criar(Usuario entidade)
        {
            
        }

        public void Excluir(Guid id)
        {
            
        }

        public IEnumerable<Usuario> Listar()
        {
            return new List<Usuario>();
        }

        public Usuario? ObterPorId(Guid id)
        {
            if (id != _idValido)
                return null;

            return new Terceirizado(
                new NomePessoaFisica("Fulano", "de Tal"),
                new Email("fulanodetal@provedor.com"),
                new NomePessoaJuridica("Empresa top"));
        }
    }
}
