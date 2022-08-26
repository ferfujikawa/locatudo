using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Testes.Repositorios;

namespace Locatudo.Testes.TestesExecutores
{
    [TestClass]
    public class TestesExecutorAprovarLocacao
    {
        private readonly Guid _idLocacaoValida = Guid.NewGuid();
        private readonly Guid _idAprovadorValido = Guid.NewGuid();
        private readonly ExecutorAprovarLocacao _executor;

        public TestesExecutorAprovarLocacao()
        {
            _executor = new ExecutorAprovarLocacao(
                new RepositorioLocacaoFalso(_idLocacaoValida),
                new RepositorioFuncionarioFalso(_idAprovadorValido)
                );
        }

        [TestMethod]
        public void Comando_valido_deve_aprovar_locacao()
        {
            var comandoValido = new ComandoAprovarLocacao(_idLocacaoValida, _idAprovadorValido);
            try
            {
                _executor.Executar(comandoValido);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
                return;
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Locacao_invalida_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoAprovarLocacao(Guid.NewGuid(), _idAprovadorValido);
            try
            {
                _executor.Executar(comandoInvalido);
            }
            catch
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }

        [TestMethod]
        public void Aprovador_invalido_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoAprovarLocacao(_idLocacaoValida, Guid.NewGuid());
            try
            {
                _executor.Executar(comandoInvalido);
            }
            catch
            {
                Assert.IsTrue(true);
                return;
            }
            Assert.IsTrue(false);
        }
    }
}
