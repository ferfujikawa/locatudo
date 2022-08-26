using Locatudo.Compartilhado.Entidades;

namespace Locatudo.Compartilhado.Repositorios
{
    public interface IRepositorio<T> where T : EntidadeAbstrata
    {
        void Criar(T entidade);
        void Excluir(Guid id);
        T? GetById(Guid id);
        IEnumerable<T> Listar();
    }
}
