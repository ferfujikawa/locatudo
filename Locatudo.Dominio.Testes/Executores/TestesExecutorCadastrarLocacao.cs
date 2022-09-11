using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Locatudo.Compartilhado.ObjetosDeValor;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;
using Locatudo.Dominio.Testes.Customizacoes;
using Moq;

namespace Locatudo.Dominio.Testes.Executores
{
    public class TestesExecutorCadastrarLocacao
    {
        private readonly ComandoCadastrarLocacao _comandoValido = new (Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddHours(1));
        private readonly ComandoCadastrarLocacao _comandoInicioPassado = new (Guid.NewGuid(), Guid.NewGuid(), DateTime.Now.AddHours(-1));

        [Theory, AutoMoq]
        public void Comando_Valido_CadastrarLocacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            var equipamento = fixture.Create<Equipamento>();
            var terceirizado = fixture.Create<Terceirizado>();
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);
            var executor = fixture.Create<ExecutorCadastrarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().NotThrow();
        }

        [Theory, AutoMoq]
        public void Comando_EquipamentoInvalido_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            var terceirizado = fixture.Create<Terceirizado>();
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Equipamento?) null);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);
            var executor = fixture.Create<ExecutorCadastrarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().Throw<Exception>("Equipamento não encontrado");
        }

        [Theory, AutoMoq]
        public void Comando_LocadorInvalido_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            var equipamento = fixture.Create<Equipamento>();
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Terceirizado?) null);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);
            var executor = fixture.Create<ExecutorCadastrarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().Throw<Exception>("Usuário não encontrado");
        }

        [Theory, AutoMoq]
        public void Comando_InicioPassado_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            var equipamento = fixture.Create<Equipamento>();
            var terceirizado = fixture.Create<Terceirizado>();
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);
            var executor = fixture.Create<ExecutorCadastrarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoInicioPassado);

            //Assert
            acao.Should().Throw<Exception>("Início não pode ser no passado");
        }

        [Theory, AutoMoq]
        public void Comando_DataIndisponivel_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            var equipamento = fixture.Create<Equipamento>();
            var terceirizado = fixture.Create<Terceirizado>();
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(false);
            var executor = fixture.Create<ExecutorCadastrarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().Throw<Exception>("Horário de locação indisponível");
        }
    }
}
