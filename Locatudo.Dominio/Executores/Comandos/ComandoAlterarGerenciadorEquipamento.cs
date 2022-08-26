using Locatudo.Compartilhado.Executores.Comandos;

namespace Locatudo.Dominio.Executores.Comandos
{
    public class ComandoAlterarGerenciadorEquipamento : IComandoExecutor
    {
        public ComandoAlterarGerenciadorEquipamento()
        {
        }

        public ComandoAlterarGerenciadorEquipamento(Guid id, Guid idDepartamento)
        {
            Id = id;
            IdDepartamento = idDepartamento;
        }

        public Guid Id { get; set; }
        public Guid IdDepartamento { get; set; }
    }
}
