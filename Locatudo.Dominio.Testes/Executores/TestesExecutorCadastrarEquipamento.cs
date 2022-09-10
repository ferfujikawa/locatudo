using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Dominio.Testes.Customizacoes;
using FluentAssertions;

namespace Locatudo.Dominio.Testes.Executores
{
    public class TestesExecutorCadastrarEquipamento
    {
        [Theory, AutoMoq]
        public void Comando_Valido_CadastrarEquipamento(
            ExecutorCadastrarEquipamento executor)
        {
            //Arrange
            var comandoValido = new ComandoCadastrarEquipamento("Equipamento Teste 123");

            //Act
            var acao = () => executor.Executar(comandoValido);

            //Assert
            acao.Should().NotThrow();
        }
    }
}
