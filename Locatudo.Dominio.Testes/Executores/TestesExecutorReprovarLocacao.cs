using AutoFixture;
using AutoFixture.Xunit2;
using FluentAssertions;
using Locatudo.Compartilhado.Enumeradores;
using Locatudo.Compartilhado.ObjetosDeValor;
using Locatudo.Dominio.Entidades;
using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Repositorios;
using Locatudo.Dominio.Testes.Customizacoes;
using Moq;

namespace Locatudo.Dominio.Testes.Executores
{
    public class TestesExecutorReprovarLocacao
    {
        [Theory, AutoMoq]
        public void Comando_Valido_ReprovarLocacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            //Resolução de dependência de classe abstrata Usuario
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());

            //Criação de Mocks
            var departamento = fixture.Create<Departamento>();
            var aprovador = fixture.Create<Funcionario>();
            var equipamento = fixture.Create<Equipamento>();
            
            //Alteração de propriedades dos mocks
            aprovador.AlterarLotacao(departamento);
            equipamento.AlterarGerenciador(departamento);

            //Criação do mock de Locacao
            fixture.Customize<Locacao>(x => x.FromFactory(() => new Locacao(equipamento, fixture.Create<Funcionario>(), fixture.Create<HorarioLocacao>())));
            var locacao = fixture.Create<Locacao>();

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorReprovarLocacao>();
            var comando = new ComandoReprovarLocacao(locacao.Id, aprovador.Id);

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().NotThrow();
            locacao.Situacao.Valor.Should().Be(ESituacaoLocacao.Reprovado, "Ao reprovar a locação, a situação deve ser alterada para Reprovado");
            locacao.Aprovador
                .Should().NotBeNull("Ao reprovar a locação, precisa registrar quem é o aprovador")
                .And.BeOfType<Funcionario>("Somente um funcionário pode reprovar uma locação")
                .Which.Id.Should().Be(comando.IdAprovador, "O reprovador da locação precisa ser o mesmo cujo Id foi passado no comando");
        }

        [Theory, AutoMoq]
        public void Locacao_Invalida_GerarExcecao(IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            //Criação de mocks
            var aprovador = fixture.Create<Funcionario>();

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Locacao?)null);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorReprovarLocacao>();
            var comando = new ComandoReprovarLocacao(Guid.NewGuid(), aprovador.Id);

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().Throw<Exception>("A reprovação de uma locação inexistente não pode ser realizada");
        }

        [Theory, AutoMoq]
        public void Aprovador_Invalido_GerarExcecao(IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            //Resolução de dependência de classe abstrata Usuario
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());

            //Criação de mocks
            var locacao = fixture.Create<Locacao>();

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Funcionario?)null);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorReprovarLocacao>();
            var comando = new ComandoReprovarLocacao(locacao.Id, Guid.NewGuid());

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().Throw<Exception>("A reprovação de uma locação não pode ser realizada por um funcionário inexistente");
        }
    }
}
