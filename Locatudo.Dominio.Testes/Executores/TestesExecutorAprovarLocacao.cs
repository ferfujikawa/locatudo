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
using Locatudo.Compartilhado.Enumeradores;

namespace Locatudo.Dominio.Testes.Executores
{
    public class TestesExecutorAprovarLocacao
    {
        [Theory, AutoMoq]
        public void Comando_Valido_AprovarLocacao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            //Resolução de dependência de classe abstrata Usuario
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());

            //Criação de mocks
            var departamento = fixture.Create<Departamento>();
            var aprovador = fixture.Create<Funcionario>();
            var equipamento = fixture.Create<Equipamento>();

            //Alteração de propriedades de mocks
            aprovador.AlterarLotacao(departamento);
            equipamento.AlterarGerenciador(departamento);

            //Criação do mock de Locacao
            fixture.Customize<Locacao>(x => x.FromFactory(() => new Locacao(equipamento, fixture.Create<Funcionario>(), fixture.Create<HorarioLocacao>())));
            var locacao = fixture.Create<Locacao>();

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorAprovarLocacao>();
            var comando = new ComandoAprovarLocacao(locacao.Id, aprovador.Id);

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().NotThrow();
            locacao.Situacao.Valor.Should().Be(ESituacaoLocacao.Aprovado, "Ao aprovar a locação, a situação deve ser alterada para Aprovado");
            locacao.Aprovador
                .Should().NotBeNull("Ao aprovar a locação, precisa registrar quem é o aprovador")
                .And.BeOfType<Funcionario>("Somente um funcionário pode aprovar uma locação")
                .Which.Id.Should().Be(comando.IdAprovador, "O aprovador da locação precisa ser o mesmo cujo Id foi passado no comando");
        }

        [Theory, AutoMoq]
        public void Locacao_Invalida_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            //Criação de mocks
            var aprovador = fixture.Create<Funcionario>();

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(aprovador);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Locacao?) null);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorAprovarLocacao>();
            var comando = new ComandoAprovarLocacao(Guid.NewGuid(), aprovador.Id);

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().Throw<Exception>("A aprovação de uma locação inexistente não pode ser realizada");
        }

        [Theory, AutoMoq]
        public void Aprovador_Invalido_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioLocacao> repositorioLocacao,
            [Frozen] Mock<IRepositorioFuncionario> repositorioFuncionario)
        {
            //Arrange
            //Resolução de dependência de classe abstrata Usuario
            fixture.Inject<Usuario>(fixture.Create<Funcionario>());

            //Criação de mocks
            var locacao = fixture.Create<Locacao>();

            //Setup de retornos de métodos dos repositórios
            repositorioFuncionario.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Funcionario?) null);
            repositorioLocacao.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(locacao);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorAprovarLocacao>();
            var comando = new ComandoAprovarLocacao(locacao.Id, Guid.NewGuid());

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().Throw<Exception>("A aprovação de uma locação não pode ser realizada por um funcionário inexistente");
        }
    }
}
