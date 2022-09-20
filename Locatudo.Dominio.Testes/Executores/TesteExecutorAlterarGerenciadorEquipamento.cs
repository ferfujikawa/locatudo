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
    public class TesteExecutorAlterarGerenciadorEquipamento
    {
        [Theory, AutoMoq]
        public void Comando_Valido_AlterarGerenciadorEquipamento(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioDepartamento> repositorioDepartamento)
        {
            //Arrange
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();
            var departamento = fixture.Create<Departamento>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioDepartamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(departamento);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorAlterarGerenciadorEquipamento>();
            var comando = new ComandoAlterarGerenciadorEquipamento(equipamento.Id, departamento.Id);

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().NotThrow();
            equipamento.Gerenciador
                .Should().Match<Departamento>(x => x.Id == departamento.Id, "O departamento gerenciador do equipamento precisa ser o mesmo cujo Id foi passado no comando");
        }

        [Theory, AutoMoq]
        public void Equipamento_Invalido_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioDepartamento> repositorioDepartamento)
        {
            //Arrange
            //Criação de mocks
            var departamento = fixture.Create<Departamento>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Equipamento?) null);
            repositorioDepartamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(departamento);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorAlterarGerenciadorEquipamento>();
            var comando = new ComandoAlterarGerenciadorEquipamento(Guid.NewGuid(), departamento.Id);

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().Throw<Exception>("A alteração de gerenciador de um equipamento inexistente não pode ser realizada");
        }

        [Theory, AutoMoq]
        public void Departamento_Invalido_GerarExcecao(
            IFixture fixture,
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioDepartamento> repositorioDepartamento)
        {
            //Arrange
            //Criação de mocks
            var equipamento = fixture.Create<Equipamento>();

            //Setup de retornos de métodos dos repositórios
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento);
            repositorioDepartamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns((Departamento?) null);

            //Mock de executor e instância de comando
            var executor = fixture.Create<ExecutorAlterarGerenciadorEquipamento>();
            var comando = new ComandoAlterarGerenciadorEquipamento(equipamento.Id, Guid.NewGuid());

            //Act
            var acao = () => executor.Executar(comando);

            //Assert
            acao.Should().Throw<Exception>("A alteração de gerenciador de um equipamento não pode ser realizada para um departamento inexistente");
        }
    }
}
