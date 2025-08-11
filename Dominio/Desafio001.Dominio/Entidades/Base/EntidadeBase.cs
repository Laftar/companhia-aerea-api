namespace Desafio001.Dominio.Entidades.Base
{
    public class EntidadeBase
    {
        public Guid Id { get; private set; } = Guid.Empty;

        public EntidadeBase()
        { }
    }
}
