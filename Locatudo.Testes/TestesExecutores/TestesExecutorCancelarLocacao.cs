using AutoFixture;
using AutoFixture.AutoMoq;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;
using Moq;

namespace Locatudo.Testes.TestesExecutores
{
    [TestClass]
    public class TestesExecutorCancelarLocacao
    {
        private readonly Guid _idLocacaoValida = Guid.NewGuid();
        private readonly ExecutorCancelarLocacao _executor;

        public TestesExecutorCancelarLocacao()
        {
            //Criação do objeto fixture
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            //Injeção de dependências
            ConfigurarRepositorioLocacaoMock(fixture);

            _executor = fixture.Create<ExecutorCancelarLocacao>();
        }

        private void ConfigurarRepositorioLocacaoMock(IFixture fixture)
        {
            //Injeção de dependência do tipo Usuario para criação da classe Locacao
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());

            //Dependência de ExecutorReprovarLocacao
            var repositorioLocacaoMock = fixture.Freeze<Mock<IRepositorioLocacao>>();

            //Setup de retorno de locação válida com id conhecido
            repositorioLocacaoMock.Setup(x => x.ObterPorId(_idLocacaoValida)).Returns(fixture.Create<Locacao>());

            //Setup de retorno null com id qualquer
            repositorioLocacaoMock.Setup(x => x.ObterPorId(It.Is<Guid>(x => x != _idLocacaoValida))).Returns((Locacao?)null);
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
            var excecao = Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Locação não encontrada.", excecao.Message);
        }
    }
}
