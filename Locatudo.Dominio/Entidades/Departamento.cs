using Locatudo.Compartilhado.Entidades;
using Locatudo.Dominio.ObjetosDeValor;

namespace Locatudo.Dominio.Entidades
{
    public class Departamento : EntidadeAbstrata
    {
        public Departamento(string nome, Email email)
        {
            Nome = nome;
            Email = email;
        }

        public string Nome { get; private set; }
        public Email Email { get; private set; }
    }
}
