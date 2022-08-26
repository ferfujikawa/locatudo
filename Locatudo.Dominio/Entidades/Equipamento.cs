using Locatudo.Compartilhado.Entidades;

namespace Locatudo.Dominio.Entidades
{
    public class Equipamento : EntidadeAbstrata
    {
        public Equipamento(string nome) : base()
        {
            Nome = nome;
        }

        public string Nome { get; private set; }
        public Departamento? DepartamentoResponsavel { get; private set; }
    }
}
