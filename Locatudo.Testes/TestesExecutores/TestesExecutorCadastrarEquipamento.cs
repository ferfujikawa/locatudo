using AutoFixture;
using AutoFixture.AutoMoq;
using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;
using Moq;

namespace Locatudo.Testes.TestesExecutores
{
    [TestClass]
    public class TestesExecutorCadastrarEquipamento
    {
        private readonly ExecutorCadastrarEquipamento _executor;
        public TestesExecutorCadastrarEquipamento()
        {
            //Criação do objeto fixture
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            //Injeção de dependências
            ConfigurarRepositorioEquipamentoMock(fixture);

            _executor = fixture.Create<ExecutorCadastrarEquipamento>();
        }

        private void ConfigurarRepositorioEquipamentoMock(IFixture fixture)
        {
            //Dependência de ExecutorReprovarLocacao
            fixture.Freeze<Mock<IRepositorioEquipamento>>();
        }

        [TestMethod]
        public void Comando_valido_deve_cadastrar_equipamento()
        {
            var comandoValido = new ComandoCadastrarEquipamento("Equipamento Teste 123");
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
    }
}
