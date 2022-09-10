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
        private readonly ComandoAlterarGerenciadorEquipamento _comandoValido = new (Guid.NewGuid(), Guid.NewGuid());

        [Theory, AutoMoq]
        public void Comando_Valido_AlterarGerenciadorEquipamento(
            [Frozen] Mock<IRepositorioEquipamento> repositorioEquipamento,
            [Frozen] Mock<IRepositorioDepartamento> repositorioDepartamento,
            Mock<Equipamento> equipamento,
            Mock<Departamento> departamento,
            ExecutorAlterarGerenciadorEquipamento executor)
        {
            //Arrange
            repositorioEquipamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(equipamento.Object);
            repositorioDepartamento.Setup(x => x.ObterPorId(It.IsAny<Guid>())).Returns(departamento.Object);
            

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().NotThrow();
        }
    }
}
