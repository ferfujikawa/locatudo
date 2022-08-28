using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Testes.Repositorios;

namespace Locatudo.Testes.TestesExecutores
{
    [TestClass]
    public class TestesExecutorCancelarLocacao
    {
        private readonly Guid _idLocacaoValida = Guid.NewGuid();
        private readonly ExecutorCancelarLocacao _executor;

        public TestesExecutorCancelarLocacao()
        {
            _executor = new ExecutorCancelarLocacao(new RepositorioLocacaoFalso(_idLocacaoValida, DateTime.Now));
        }

        [TestMethod]
        public void Comando_valido_deve_aprovar_locacao()
        {
            var comandoValido = new ComandoCancelarLocacao(_idLocacaoValida);
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
        public void Locacao_invalida_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoCancelarLocacao(Guid.NewGuid());
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
