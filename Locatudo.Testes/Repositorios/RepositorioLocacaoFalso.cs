using Locatudo.Dominio.Entidades;
using Locatudo.Compartilhado.ObjetosDeValor;
using Locatudo.Dominio.Repositorios;

namespace Locatudo.Testes.Repositorios
{
    public class RepositorioLocacaoFalso : IRepositorioLocacao
    {
        private readonly Guid _idValido = Guid.NewGuid();

        public RepositorioLocacaoFalso(Guid idValido)
        {
            _idValido = idValido;
        }

        public void Alterar(Locacao entidade)
        {
            
        }

        public void Criar(Locacao entidade)
        {
            
        }

        public void Excluir(Guid id)
        {
            
        }

        public IEnumerable<Locacao> Listar()
        {
            return new List<Locacao>();
        }

        public Locacao? ObterPorId(Guid id)
        {
            if (id != _idValido)
                return null;

            var equipamento = new Equipamento("Equipamento teste");
            var locatario = new Terceirizado(
                new NomePessoaFisica("Fulano", "de Tal"),
                new Email("fulanodetal@provedor.com"),
                new NomePessoaJuridica("Empresa top")
                );
            var horario = new HorarioLocacao(DateTime.Now);
            return new Locacao(equipamento, locatario, horario);
        }
    }
}
