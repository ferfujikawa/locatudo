using Locatudo.Compartilhado.Executores.Comandos;

namespace Locatudo.Dominio.Executores.Comandos
{
    public class ComandoCadastrarEquipamento : IComandoExecutor
    {
        public ComandoCadastrarEquipamento()
        {
        }

        public ComandoCadastrarEquipamento(string nome)
        {
            Nome = nome;
        }

        public string Nome { get; set; }
    }
}
