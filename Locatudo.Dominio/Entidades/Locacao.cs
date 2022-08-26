using Locatudo.Compartilhado.Entidades;
using Locatudo.Dominio.ObjetosDeValor;

namespace Locatudo.Dominio.Entidades
{
    public class Locacao : EntidadeAbstrata
    {
        public Locacao(Equipamento equipamento, Usuario locatario, Funcionario aprovador, SituacaoLocacao situacao, HorarioLocacao horario)
        {
            Equipamento = equipamento;
            Locatario = locatario;
            Aprovador = aprovador;
            Situacao = situacao;
            Horario = horario;
        }

        public Equipamento Equipamento { get; private set; }
        public Usuario Locatario { get; private set; }
        public Funcionario Aprovador { get; private set; }
        public SituacaoLocacao Situacao { get; private set; }
        public HorarioLocacao Horario { get; private set; }
    }
}
