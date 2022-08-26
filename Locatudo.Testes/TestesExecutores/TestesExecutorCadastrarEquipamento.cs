using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Testes.Repositorios;

namespace Locatudo.Testes.TestesExecutores
{
    [TestClass]
    public class TestesExecutorCadastrarEquipamento
    {
        private readonly ComandoCadastrarEquipamento _comandoValido = new ComandoCadastrarEquipamento("Equipamento Teste 123");
        private readonly ExecutorCadastrarEquipamento _executor = new ExecutorCadastrarEquipamento(new RepositorioEquipamentoFalso());

        [TestMethod]
        public void Comando_valido_deve_cadastrar_equipamento()
        {
            try
            {
                _executor.Executar(_comandoValido);
                Assert.IsTrue(true);
            }
            catch
            {
                Assert.IsTrue(false);
            }
        }
    }
}
