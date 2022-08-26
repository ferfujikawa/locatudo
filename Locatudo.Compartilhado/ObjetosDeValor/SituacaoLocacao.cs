using Locatudo.Compartilhado.Enumeradores;

namespace Locatudo.Compartilhado.ObjetosDeValor
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
