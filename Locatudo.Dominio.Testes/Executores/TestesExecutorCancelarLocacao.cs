using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;
using Locatudo.Dominio.Testes.Customizacoes;
using Moq;

namespace Locatudo.Dominio.Testes.Executores
{
    public class TestesExecutorCancelarLocacao
    {
        private readonly ComandoCancelarLocacao _comandoValido = new (Guid.NewGuid());

        [Theory, AutoMoq]
        public void Comando_Valido_AprovarLocacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(fixture.Create<Locacao>());
            var executor = fixture.Create<ExecutorCancelarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().NotThrow();
        }

        [Theory, AutoMoq]
        public void Locacao_Invalida_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Locacao?)null);
            var executor = fixture.Create<ExecutorCancelarLocacao>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().Throw<Exception>("Locação não encontrada.");
        }
    }
}
