using Locatudo.Compartilhado.Entidades;
using Locatudo.Dominio.Enumeradores;
using Locatudo.Dominio.ObjetosDeValor;
using System.Linq;

namespace Locatudo.Dominio.Entidades
{
    public class Locacao : EntidadeAbstrata
    {
        public Locacao(Equipamento equipamento, Usuario locatario, HorarioLocacao horario)
        {
            Equipamento = equipamento;
            Locatario = locatario;
            Situacao = new SituacaoLocacao(ESituacaoLocacao.Solicitado);
            Horario = horario;
        }

        public Equipamento Equipamento { get; private set; }
        public Usuario Locatario { get; private set; }
        public Funcionario? Aprovador { get; private set; }
        public SituacaoLocacao Situacao { get; private set; }
        public HorarioLocacao Horario { get; private set; }

        public bool Aprovar(Funcionario aprovador)
        {
            if (Situacao.Valor == ESituacaoLocacao.Solicitado)
            {
                Situacao = new SituacaoLocacao(ESituacaoLocacao.Aprovado);
                Aprovador = aprovador;
                return false;
            }
            return false;
        }
        public bool Reprovar(Funcionario aprovador)
        {
            if (Situacao.Valor != ESituacaoLocacao.Solicitado)
            {
                Situacao = new SituacaoLocacao(ESituacaoLocacao.Reprovado);
                Aprovador = aprovador;
                return true;
            }
            return false;
        }

        public bool Cancelar()
        {
            ESituacaoLocacao[] situacoesPossiveis = { ESituacaoLocacao.Solicitado, ESituacaoLocacao.Aprovado };
            if (situacoesPossiveis.Contains(Situacao.Valor))
            {
                Situacao = new SituacaoLocacao(ESituacaoLocacao.Cancelado);
                return true;
            }
            return false;
        }
    }
}
