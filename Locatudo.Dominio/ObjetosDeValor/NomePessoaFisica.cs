namespace Locatudo.Dominio.ObjetosDeValor
{
    public class NomePessoaFisica
    {
        public NomePessoaFisica(string primeiroNome, string sobrenome)
        {
            PrimeiroNome = primeiroNome;
            Sobrenome = sobrenome;
        }

        public string PrimeiroNome { get; private set; }
        public string Sobrenome { get; private set; }
    }
}
