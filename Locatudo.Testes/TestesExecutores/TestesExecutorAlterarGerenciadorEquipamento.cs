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
    public class TestesExecutorAlterarGerenciadorEquipamento
    {
        private readonly Guid _idEquipamentoValido = Guid.NewGuid();
        private readonly Guid _idDepartamentoValido = Guid.NewGuid();
        private readonly ExecutorAlterarGerenciadorEquipamento _executor;

        public TestesExecutorAlterarGerenciadorEquipamento()
        {
            //Criação do objeto fixture
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            //Injeção de dependências
            ConfigurarRepositorioEquipamentoMock(fixture);
            ConfigurarRepositorioDepartamentoMock(fixture);

            _executor = fixture.Create<ExecutorAlterarGerenciadorEquipamento>();
        }

        private void ConfigurarRepositorioEquipamentoMock(IFixture fixture)
        {
            //Dependência do executor
            var repositorioEquipamento = fixture.Freeze<Mock<IRepositorioEquipamento>>();

            //Setup de retorno de equipamento válido com id conhecido
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(fixture.Create<Equipamento>());

            //Setup de retorno null com id qualquer
            repositorioEquipamento.Setup(x => x.ObterPorId(It.Is<Guid>(x => x != _idEquipamentoValido))).Returns((Equipamento?)null);
        }

        private void ConfigurarRepositorioDepartamentoMock(IFixture fixture)
        {
            //Dependência do executor
            var repositorioDepartamento = fixture.Freeze<Mock<IRepositorioDepartamento>>();

            //Setup de retorno de departamento válido com id conhecido
            repositorioDepartamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(fixture.Create<Departamento>());

            //Setup de retorno null com id qualquer
            repositorioDepartamento.Setup(x => x.ObterPorId(It.Is<Guid>(x => x != _idDepartamentoValido))).Returns((Departamento?)null);
        }

        [TestMethod]
        public void Comando_valido_deve_alterar_gerenciador_equipamento()
        {
            var comandoValido = new ComandoAlterarGerenciadorEquipamento(_idEquipamentoValido, _idDepartamentoValido);
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
        public void Departamento_invalido_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoAlterarGerenciadorEquipamento(_idEquipamentoValido, Guid.NewGuid());
            var mensagem = Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Departamento não encontrado", mensagem.Message);
        }

        [TestMethod]
        public void Equipamento_invalido_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoAlterarGerenciadorEquipamento(Guid.NewGuid(), _idDepartamentoValido);
            var mensagem = Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Equipamento não encontrado", mensagem.Message);
        }
    }
}
