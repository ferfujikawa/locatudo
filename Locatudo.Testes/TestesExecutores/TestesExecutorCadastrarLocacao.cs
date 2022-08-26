using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Testes.Repositorios;

namespace Locatudo.Testes.TestesExecutores
{
    [TestClass]
    public class TestesExecutorCadastrarLocacao
    {
        private readonly Guid _idEquipamentoValido = Guid.NewGuid();
        private readonly Guid _idUsuarioValido= Guid.NewGuid();
        private readonly ExecutorCadastrarLocacao _executor;

        public TestesExecutorCadastrarLocacao()
        {
            _executor = new ExecutorCadastrarLocacao(
                new RepositorioEquipamentoFalso(_idEquipamentoValido),
                new RepositorioUsuarioFalso(_idUsuarioValido),
                new RepositorioLocacaoFalso(Guid.NewGuid())
                );
        }

        [TestMethod]
        public void Comando_valido_deve_cadastrar_locacao()
        {
            var comandoValido = new ComandoCadastrarLocacao(_idEquipamentoValido, _idUsuarioValido, DateTime.Now.AddHours(1));
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
            var comandoInvalido = new ComandoCadastrarLocacao(Guid.NewGuid(), _idUsuarioValido, DateTime.Now.AddHours(1));
            try
            {
                _executor.Executar(comandoInvalido);
            }
            catch
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void Locador_invalido_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoCadastrarLocacao(_idEquipamentoValido, Guid.NewGuid(), DateTime.Now.AddHours(1));
            try
            {
                _executor.Executar(comandoInvalido);
            }
            catch
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void Data_invalida_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoCadastrarLocacao(_idEquipamentoValido, _idUsuarioValido, DateTime.Now.AddHours(-1));
            try
            {
                _executor.Executar(comandoInvalido);
            }
            catch
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.Fail();
        }
    }
}
