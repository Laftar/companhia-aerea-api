namespace Desafio001.Dominio.Configuracoes
{
    public class SmtpConfig
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; }
        public string User { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = true;
        public string From { get; set; } = string.Empty;
        public string FromName { get; set; } = string.Empty;
        public bool IsBodyHtml { get; set; } = false;
    }
}
