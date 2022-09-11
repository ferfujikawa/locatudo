using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Testes.Customizacoes;
using FluentAssertions;
using AutoFixture;

namespace Locatudo.Dominio.Testes.Executores
{
    public class TestesExecutorCadastrarEquipamento
    {
        private readonly ComandoCadastrarEquipamento _comandoValido = new ("Equipamento Teste 123");

        [Theory, AutoMoq]
        public void Comando_Valido_CadastrarEquipamento(IFixture fixture)
        {
            //Arrange
            var executor = fixture.Create<ExecutorCadastrarEquipamento>();

            //Act
            var acao = () => executor.Executar(_comandoValido);

            //Assert
            acao.Should().NotThrow();
        }
    }
}
