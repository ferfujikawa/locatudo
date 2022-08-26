using Locatudo.Compartilhado.Entidades;

namespace Locatudo.Compartilhado.Repositorios
{
    public interface IRepositorio<T> where T : EntidadeAbstrata
    {
        void Criar(T entidade);
        IEnumerable<T> Listar();
        T? ObterPorId(Guid id);
        void Alterar(T entidade);
        void Excluir(Guid id);
    }
}
