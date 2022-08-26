using Locatudo.Dominio.Enumeradores;

namespace Locatudo.Dominio.ObjetosDeValor
{
    public class SituacaoLocacao
    {
        public SituacaoLocacao(ESituacaoLocacao valor)
        {
            Valor = valor;
        }

        public ESituacaoLocacao Valor { get; private set; }
    }
}
