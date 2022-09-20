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
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();
            var terceirizado = fixture.Create<Terceirizado>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);

            //Criação do mock do executor
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
            //Criação de mocks
            var terceirizado = fixture.Create<Terceirizado>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Equipamento?) null);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);

            //Criação do mock do executor
            var executor = fixture.Create<ExecutorCadastrarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().Throw<Exception>("A locação de um equipamento inexistente não pode ser realizada");
        }

        [Theory, AutoMoq]
        public void Comando_LocadorInvalido_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Terceirizado?) null);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);

            //Criação do mock do executor
            var executor = fixture.Create<ExecutorCadastrarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().Throw<Exception>("A locação de um equipamento não pode ser realizada para um usuário inexistente");
        }

        [Theory, AutoMoq]
        public void Comando_InicioPassado_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();
            var terceirizado = fixture.Create<Terceirizado>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(true);

            //Criação do mock do executor
            var executor = fixture.Create<ExecutorCadastrarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoInicioPassado);

            //Assert
            acao.Should().Throw<Exception>("A locação de um equipamento não pode ser realizada para um horário no passado");
        }

        [Theory, AutoMoq]
        public void Comando_DataIndisponivel_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioUsuario> repositorioUsuario,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();
            var terceirizado = fixture.Create<Terceirizado>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioUsuario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(terceirizado);
            repositorioLocacao.Setup(x => x.VerificarDisponibilidade(It.IsAny<Guid>(), It.IsAny<HorarioLocacao>())).Returns(false);

            //Criação do mock do executor
            var executor = fixture.Create<ExecutorCadastrarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().Throw<Exception>("A locação de um equipamento não pode ser realizada para um horário indisponível");
        }
    }
}
