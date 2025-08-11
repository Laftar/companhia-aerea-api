namespace Desafio001.Dominio.Entidades.Base
{
    public abstract class EntidadeComExclusaoLogica : EntidadeBase
    {
        public bool Excluido { get; private set; }

        protected EntidadeComExclusaoLogica()
        {
            Excluido = false;
        }

        public void Excluir()
        {
            Excluido = true;
        }

    }
}
