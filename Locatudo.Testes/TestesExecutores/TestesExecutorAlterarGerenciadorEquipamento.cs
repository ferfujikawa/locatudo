using Locatudo.Dominio.Executores;
using Locatudo.Dominio.Executores.Comandos;
using Locatudo.Testes.Repositorios;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;

namespace Locatudo.Testes.TestesExecutores
{
    [TestClass]
    public class TestesExecutorAlterarGerenciadorEquipamento
    {
        private readonly Guid _idEquipamentoValido = Guid.NewGuid();
        private readonly Guid _idDepartamentoValido = Guid.NewGuid();

        [TestMethod]
        public void Comando_valido_deve_alterar_gerenciador_equipamento()
        {
            var comandoValido = new ComandoAlterarGerenciadorEquipamento(_idEquipamentoValido, _idDepartamentoValido);
            var executor = new ExecutorAlterarGerenciadorEquipamento(
                new RepositorioEquipamentoFalso(_idEquipamentoValido),
                new RepositorioDepartamentoFalso(_idDepartamentoValido)
                );
            try
            {
                executor.Executar(comandoValido);
            }
            catch (Exception ex)
            {
                Assert.Fail(ex.Message);
                return;
            }
            Assert.IsTrue(true);
        }

        [TestMethod]
        public void Departamento_invalido_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoAlterarGerenciadorEquipamento(_idEquipamentoValido, Guid.NewGuid());
            var executor = new ExecutorAlterarGerenciadorEquipamento(
                new RepositorioEquipamentoFalso(_idEquipamentoValido),
                new RepositorioDepartamentoFalso(_idDepartamentoValido)
                );
            var mensagem = Assert.ThrowsException<Exception>(
                () => executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Departamento não encontrado", mensagem.Message);
        }

        [TestMethod]
        public void Equipamento_invalido_deve_gerar_excecao()
        {
            var comandoInvalido = new ComandoAlterarGerenciadorEquipamento(Guid.NewGuid(), _idDepartamentoValido);
            var executor = new ExecutorAlterarGerenciadorEquipamento(
                new RepositorioEquipamentoFalso(_idEquipamentoValido),
                new RepositorioDepartamentoFalso(_idDepartamentoValido)
                );
            var mensagem = Assert.ThrowsException<Exception>(
                () => executor.Executar(comandoInvalido)
            );
            Assert.AreEqual("Equipamento não encontrado", mensagem.Message);
        }
    }
}
