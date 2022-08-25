namespace Locatudo.Dominio.ObjetosDeValor
{
    public class NomePessoaJuridica
    {
        public NomePessoaJuridica(string razaoSocial)
        {
            RazaoSocial = razaoSocial;
        }

        public string RazaoSocial { get; private set; }
    }
}
