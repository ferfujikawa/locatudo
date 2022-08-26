using Locatudo.Compartilhado.Executores.Comandos;

namespace Locatudo.Compartilhado.Executores
{
    public interface IExecutor<T> where T : IComandoExecutor
    {
        void Executar(T comando);
    }
}
