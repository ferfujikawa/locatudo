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
    public class TestesExecutorCadastrarLocacao
    {
        private readonly Guid _idEquipamentoValido = Guid.NewGuid();
        private readonly Guid _idUsuarioValido = Guid.NewGuid();
        private readonly HorarioLocacao _horarioDisponivel = new(DateTime.Now.AddHours(1));
        private readonly ExecutorCadastrarLocacao _executor;

        public TestesExecutorCadastrarLocacao()
        {
            //Criação do objeto Fixture
            var fixture = new Fixture().Customize(new AutoMoqCustomization());

            //Injeção de Dependências
            CarregarRepositorioEquipamento(fixture);
            CarregarRepositorioUsuario(fixture);
            CarregarRepositorioLocacao(fixture);

            _executor = fixture.Create<ExecutorCadastrarLocacao>();
        }

        private void CarregarRepositorioEquipamento(IFixture fixture)
        {
            //Injeção de mock de RepositorioEquipamento
            var repositorioEquipamento = fixture.Freeze<Mock<IRepositorioEquipamento>>();

            //Configuração de retorno de ObterPorId com id válido
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(fixture.Create<Equipamento>());

            //Configuração de retorno de ObterPorId com id inválido
            repositorioEquipamento.Setup(x => x.ObterPorId(It.Is<Guid>(x => x != _idEquipamentoValido))).Returns((Equipamento?) null);
        }

        private void CarregarRepositorioUsuario(IFixture fixture)
        {
            //Injeção de mock de RepositorioUsuario
            var repositorioUsuario = fixture.Freeze<Mock<IRepositorioUsuario>>();

            //Configuração de retorno de ObterPorId
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(fixture.Create<Terceirizado>());

            //Configuração de retorno de ObterPorId com id inválido
            repositorioUsuario.Setup(x => x.ObterPorId(It.Is<Guid>(x => x != _idUsuarioValido))).Returns((Terceirizado?)null);
        }

        private void CarregarRepositorioLocacao(IFixture fixture)
        {
            //Injeção de mock de RepositorioLocacao
            var repositorioLocacao = fixture.Freeze<Mock<IRepositorioLocacao>>();

            //Configuração de retorno de VerificarDisponibilidade com data disponível
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);

            //Configuração de retorno de VerificarDisponibilidade com data indisponível
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.Is<HorarioLocacao>(x => x.Inicio != _horarioDisponivel.Inicio))).Returns(false);
        }

        [TestMethod]
        public void Comando_valido_deve_cadastrar_locacao()
        {
            var comandoValido = new ComandoCadastrarLocacao(_idEquipamentoValido, _idUsuarioValido, _horarioDisponivel.Inicio);
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
            var comandoInvalido = new ComandoCadastrarLocacao(Guid.NewGuid(), _idUsuarioValido, _horarioDisponivel.Inicio);
            var excecao = Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Equipamento não encontrado", excecao.Message);
        }

        [TestMethod]
        public void Locador_invalido_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoCadastrarLocacao(_idEquipamentoValido, Guid.NewGuid(), _horarioDisponivel.Inicio);
            var excecao = Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Usuário não encontrado", excecao.Message);
        }

        [TestMethod]
        public void Data_invalida_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoCadastrarLocacao(_idEquipamentoValido, _idUsuarioValido, DateTime.Now.AddHours(-1));
            var excecao = Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Início não pode ser no passado", excecao.Message);
        }

        [TestMethod]
        public void Data_indisponivel_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoCadastrarLocacao(_idEquipamentoValido, _idUsuarioValido, DateTime.Now.AddHours(2));
            var excecao = Assert.ThrowsException<Exception>(
                () => _executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Horário de locação indisponível", excecao.Message);
        }
    }
}
