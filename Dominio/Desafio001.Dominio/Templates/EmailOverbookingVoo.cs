
using Desafio001.Dominio.Templates.Base;

namespace Desafio001.Dominio.Templates
{
    public class EmailOverbookingVoo : EmailTemplateBase
    {
        public string NomePassageiro { get; set; } = string.Empty;
        public Guid VooId { get; set; }


        public EmailOverbookingVoo(string Destinatario) : base(Destinatario) { }

        public override string Assunto => "Notificação de Overbooking";

        public override string GerarCorpo()
        {
            return $@"
            Prezado(a) {NomePassageiro},

            Identificamos um overbooking no seu voo {VooId}. 
            Favor entrar em contato com nosso suporte para mais informações.

            Atenciosamente,
            Companhia Aérea
            ";
        }
    }
}
