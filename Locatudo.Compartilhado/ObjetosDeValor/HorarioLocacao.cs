namespace Locatudo.Compartilhado.ObjetosDeValor
{
    public class HorarioLocacao
    {
        public HorarioLocacao(DateTime inicio)
        {
            Inicio = inicio;
        }

        public DateTime Inicio { get; private set; }
    }
}
