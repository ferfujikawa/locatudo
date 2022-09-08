using AutoFixture;
using AutoFixture.AutoMoq;
using Locatudo.Compartilhado.ObjetosDeValor;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;
using Moq;

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
            //Criação do objeto fixture
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            //Criação de Mocks
            var departamento = fixture.Create<Departamento>();
            var equipamento = fixture.Create<Equipamento>();
            equipamento.AlterarGerenciador(departamento);
            var locatario = fixture.Create<Terceirizado>();
            var locacao = CarregarLocacaoMock(fixture, equipamento, locatario);

            //Injeção de dependências
            ConfigurarRepositorioLocacaoMock(fixture, locacao);
            ConfigurarRepositorioFuncionarioMock(fixture, departamento);

            _executor = fixture.Create<ExecutorReprovarLocacao>();
        }

        private Locacao CarregarLocacaoMock(IFixture fixture, Equipamento equipamento, Terceirizado locatario)
        {
            //Criação de HorarioLocacao válido
            fixture.Customize<HorarioLocacao>(x => x.FromFactory(() => new HorarioLocacao(DateTime.Now)));
            var horario = fixture.Create<HorarioLocacao>();

            //Criação de Locacao
            fixture.Customize<Locacao>(x => x.FromFactory(() => new Locacao(equipamento, locatario, horario)));
            var locacao = fixture.Create<Locacao>();
            return locacao;
        }

        private void ConfigurarRepositorioFuncionarioMock(IFixture fixture, Departamento lotacao)
        {
            //Criação de funcionário aprovador lotado no departamento gerenciador do departamento
            var aprovador = fixture.Create<Funcionario>();
            aprovador.AlterarLotacao(lotacao);

            //Dependência de ExecutorReprovarLocacao
            var repositorioFuncionario = fixture.Freeze<Mock<IRepositorioFuncionario>>();
            
            //Setup de retorno de funcionário válido com id conhecido
            repositorioFuncionario.Setup(x => x.ObterPorId(_idAprovadorValido)).Returns(aprovador);

            //Setup de retorno null com id qualquer
            repositorioFuncionario.Setup(x => x.ObterPorId(It.Is<Guid>(x => x != _idAprovadorValido))).Returns((Funcionario?)null);
        }

        private void ConfigurarRepositorioLocacaoMock(IFixture fixture, Locacao locacao)
        {
            //Dependência de ExecutorReprovarLocacao
            var repositorioLocacaoMock = fixture.Freeze<Mock<IRepositorioLocacao>>();

            //Setup de retorno de locação válida com id conhecido
            repositorioLocacaoMock.Setup(x => x.ObterPorId(_idLocacaoValida)).Returns(locacao);

            //Setup de retorno null com id qualquer
            repositorioLocacaoMock.Setup(x => x.ObterPorId(It.Is<Guid>(x => x != _idLocacaoValida))).Returns((Locacao?)null);
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
            Assert.AreEqual("Locação não encontrada.", excecao.Message);
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
