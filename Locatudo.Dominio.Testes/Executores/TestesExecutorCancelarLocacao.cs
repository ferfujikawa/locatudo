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
        [Theory, AutoMoq]
        public void Comando_Valido_AprovarLocacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            ExecutorCancelarLocacao executor)
        {
            //Arrange
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(fixture.Create<Locacao>());
            var comandoValido = new ComandoCancelarLocacao(Guid.NewGuid());

            //Act
            var acao = () => executor.Executar(comandoValido);

            //Assert
            acao.Should().NotThrow();
        }

        [Theory, AutoMoq]
        public void Locacao_Invalida_GerarExcecao(
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            ExecutorCancelarLocacao executor)
        {
            //Arrange
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Locacao?)null);
            var comandoInvalido = new ComandoCancelarLocacao(Guid.NewGuid());

            //Act
            var acao = () => executor.Executar(comandoInvalido);

            //Assert
            acao.Should().Throw<Exception>("Locação não encontrada.");
        }
    }
}
