using Locatudo.Compartilhado.Entidades;

namespace Locatudo.Dominio.Entidades
{
    public class Equipamento : EntidadeAbstrata
    {
        public Equipamento(string nome, Departamento departamentoResponsavel) : base()
        {
            Nome = nome;
            DepartamentoResponsavel = departamentoResponsavel;
        }

        public string Nome { get; private set; }
        public Departamento DepartamentoResponsavel { get; private set; }
    }
}
