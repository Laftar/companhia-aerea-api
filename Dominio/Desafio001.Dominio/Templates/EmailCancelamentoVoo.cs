

using Desafio001.Dominio.Templates.Base;

namespace Desafio001.Dominio.Templates
{
    public class EmailCancelamentoVoo : EmailTemplateBase
    {
        public string NomePassageiro { get; set; } = string.Empty;
        public Guid VooId { get; set; }

        public EmailCancelamentoVoo(string Destinatario) : base(Destinatario) { }

        public override string Assunto => "Notificação de Cancelamento";

        public override string GerarCorpo()
        {
            return $@"
            Prezado(a) {NomePassageiro},

            Identificamos o cancelamento do seu voo {VooId}. 
            Favor entrar em contato com nosso suporte para mais informações.

            Atenciosamente,
            Companhia Aérea
            ";
        }
    }
}

