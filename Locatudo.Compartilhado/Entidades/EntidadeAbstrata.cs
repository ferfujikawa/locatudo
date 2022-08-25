namespace Locatudo.Compartilhado.Entidades
{
    public abstract class EntidadeAbstrata
    {
        protected EntidadeAbstrata()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; private set; }
    }
}
