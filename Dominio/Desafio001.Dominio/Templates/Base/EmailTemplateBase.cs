namespace Desafio001.Dominio.Templates.Base
{
    public abstract class EmailTemplateBase
    {
        public string Destinatario { get; private set; }
        public abstract string Assunto { get; }
        public abstract string GerarCorpo();

        protected EmailTemplateBase(string destinatario)
        {
            if (string.IsNullOrWhiteSpace(destinatario))
                throw new ArgumentException("O destinatário do e-mail não pode ser vazio.", nameof(destinatario));

            Destinatario = destinatario;
        }

    }
}