﻿using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Testes.Repositorios;

namespace Locatudo.Testes.TestesExecutores
{
    [TestClass]
    public class TestesExecutorCadastrarLocacao
    {
        private readonly Guid _idEquipamentoValido = Guid.NewGuid();
        private readonly Guid _idUsuarioValido = Guid.NewGuid();
        private readonly DateTime _dataDisponivel = DateTime.Now.AddHours(1);
        private readonly ExecutorCadastrarLocacao _executor;

        public TestesExecutorCadastrarLocacao()
        {
            _executor = new ExecutorCadastrarLocacao(
                new RepositorioEquipamentoFalso(_idEquipamentoValido),
                new RepositorioUsuarioFalso(_idUsuarioValido),
                new RepositorioLocacaoFalso(Guid.NewGuid(), _dataDisponivel)
                );
        }

        [TestMethod]
        public void Comando_valido_deve_cadastrar_locacao()
        {
            var comandoValido = new ComandoCadastrarLocacao(_idEquipamentoValido, _idUsuarioValido, _dataDisponivel);
            try
            {
                _executor.Executar(comandoValido);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                return;
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Equipamento_invalido_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoCadastrarLocacao(Guid.NewGuid(), _idUsuarioValido, _dataDisponivel);
            Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
        }

        [TestMethod]
        public void Locador_invalido_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoCadastrarLocacao(_idEquipamentoValido, Guid.NewGuid(), _dataDisponivel);
            Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
        }

        [TestMethod]
        public void Data_invalida_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoCadastrarLocacao(_idEquipamentoValido, _idUsuarioValido, DateTime.Now.AddHours(-1));
            Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
        }

        [TestMethod]
        public void Data_indisponivel_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoCadastrarLocacao(_idEquipamentoValido, _idUsuarioValido, DateTime.Now.AddHours(2));
            Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
        }
    }
}
