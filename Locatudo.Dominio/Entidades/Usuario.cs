﻿using Locatudo.Compartilhado.Entidades;
using Locatudo.Dominio.ObjetosDeValor;

namespace Locatudo.Dominio.Entidades
{
    public class Usuario : EntidadeAbstrata
    {
        public Usuario(NomePessoaFisica nome, Email email) : base()
        {
            Nome = nome;
            Email = email;
        }

        public NomePessoaFisica Nome { get; private set; }
        public Email Email { get; private set; }
    }
}