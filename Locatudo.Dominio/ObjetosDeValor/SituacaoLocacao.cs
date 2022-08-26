using Locatudo.Dominio.Enumeradores;

namespace Locatudo.Dominio.ObjetosDeValor
{
    public class SituacaoLocacao
    {
        public SituacaoLocacao(ESituacaoLocacao situacao)
        {
            Situacao = situacao;
        }

        public ESituacaoLocacao Situacao { get; private set; }
    }
}
