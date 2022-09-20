using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Locatudo.Compartilhado.Enumeradores;
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
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Resolução de dependência de classe abstrata Usuario
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());

            //Criação de mocks
            var locacao = fixture.Create<Locacao>();

            //Setup de retornos de métodos dos repositórios
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorCancelarLocacao>();
            var comando = new ComandoCancelarLocacao(locacao.Id);

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().NotThrow();
            locacao.Situacao.Valor.Should().Be(ESituacaoLocacao.Cancelado, "Ao cancelar a locação, a situação deve ser alterada para Cancelado");
        }

        [Theory, AutoMoq]
        public void Locacao_Invalida_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao)
        {
            //Arrange
            //Setup de retornos de métodos dos repositórios
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Locacao?)null);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorCancelarLocacao>();
            var comando = new ComandoCancelarLocacao(Guid.NewGuid());

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().Throw<Exception>("O cancelamento de uma locação inexistente não pode ser realizado");
        }
    }
}
