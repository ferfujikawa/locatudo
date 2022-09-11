using AutoFixture;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;
using Moq;
using Locatudo.Dominio.Testes.Customizacoes;
using FluentAssertions;
using AutoFixture.Xunit2;
using Locatudo.Compartilhado.ObjetosDeValor;

namespace Locatudo.Dominio.Testes.Executores
{
    public class TestesExecutorAprovarLocacao
    {
        private readonly ComandoAprovarLocacao _comandoValido = new (Guid.NewGuid(), Guid.NewGuid());

        [Theory, AutoMoq]
        public void Comando_Valido_AprovarLocacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());
            var departamento = fixture.Create<Departamento>();
            var aprovador = fixture.Create<Funcionario>();
            aprovador.AlterarLotacao(departamento);
            var equipamento = fixture.Create<Equipamento>();
            equipamento.AlterarGerenciador(departamento);
            fixture.Customize<Locacao>(x => x.FromFactory(() => new Locacao(equipamento, fixture.Create<Funcionario>(), fixture.Create<HorarioLocacao>())));
            var locacao = fixture.Create<Locacao>();
            
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);
            var executor = fixture.Create<ExecutorAprovarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().NotThrow();
        }

        [Theory, AutoMoq]
        public void Locacao_Invalida_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            var aprovador = fixture.Create<Funcionario>();
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Locacao?) null);
            var executor = fixture.Create<ExecutorAprovarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().Throw<Exception>("Locação não encontrada.");
        }

        [Theory, AutoMoq]
        public void Aprovador_Invalido_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());
            var locacao = fixture.Create<Locacao>();
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Funcionario?) null);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);
            var executor = fixture.Create<ExecutorAprovarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().Throw<Exception>("Funcionário não encontrado.");
        }
    }
}
