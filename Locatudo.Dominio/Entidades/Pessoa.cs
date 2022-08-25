using Locatudo.Compartilhado.Entidades;
using Locatudo.Dominio.ObjetosDeValor;

namespace Locatudo.Dominio.Entidades
{
    public class Pessoa : EntidadeAbstrata
    {
        public Pessoa(Nome nome, Email email)
        {
            Nome = nome;
            Email = email;
        }

        public Nome Nome { get; private set; }
        public Email Email { get; private set; }
    }
}
