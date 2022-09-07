using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Testes.Repositorios;

namespace Locatudo.Testes.TestesExecutores
{
    [TestClass]
    public class TestesExecutorReprovarLocacao
    {
        private readonly Guid _idLocacaoValida = Guid.NewGuid();
        private readonly Guid _idAprovadorValido = Guid.NewGuid();
        private readonly ExecutorReprovarLocacao _executor;

        public TestesExecutorReprovarLocacao()
        {
            _executor = new ExecutorReprovarLocacao(
                new RepositorioLocacaoFalso(_idLocacaoValida, DateTime.Now),
                new RepositorioFuncionarioFalso(_idAprovadorValido)
                );
        }

        [TestMethod]
        public void Comando_valido_deve_reprovar_locacao()
        {
            var comandoValido = new ComandoReprovarLocacao(_idLocacaoValida, _idAprovadorValido);
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
            var comandoInvalido = new ComandoReprovarLocacao(Guid.NewGuid(), _idAprovadorValido);
            var excecao = Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Locacação não encontrada.", excecao.Message);
        }

        [TestMethod]
        public void Aprovador_invalido_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoReprovarLocacao(_idLocacaoValida, Guid.NewGuid());
            var excecao = Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Funcionário não encontrado.", excecao.Message);
        }
    }
}
