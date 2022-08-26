using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Testes.Repositorios;

namespace Locatudo.Testes.TestesExecutores
{
    [TestClass]
    public class TestesExecutorCadastrarEquipamento
    {
        [TestMethod]
        public void Comando_valido_deve_cadastrar_equipamento()
        {
            var comandoValido = new ComandoCadastrarEquipamento("Equipamento Teste 123");
            var executor = new ExecutorCadastrarEquipamento(new RepositorioEquipamentoFalso(Guid.NewGuid()));
            try
            {
                executor.Executar(comandoValido);
            }
            catch (Exception ex)
            {
                Assert.IsTrue(false, ex.Message);
                return;
            }
            Assert.IsTrue(true);
        }
    }
}
