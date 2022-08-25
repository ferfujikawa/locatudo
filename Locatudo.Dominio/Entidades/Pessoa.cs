using Locatudo.Compartilhado.Entidades;

namespace Locatudo.Dominio.Entidades
{
    public class Pessoa : EntidadeAbstrata
    {
        public Pessoa(string nome, string email)
        {
            Nome = nome;
            Email = email;
        }

        public string Nome { get; private set; }
        public string Email { get; private set; }
    }
}
