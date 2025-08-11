using Desafio001.Dominio.Configuracoes;
using Desafio001.Dominio.Templates.Base;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;

namespace Desafio001.Servico.Servicos
{
    public class EmailServico : IEmailServico
    {
        private readonly SmtpConfig config;
        private readonly ILogger<EmailServico> logger;

        public EmailServico(
            IOptions<SmtpConfig> config,
            ILogger<EmailServico> logger)
        {
            this.config = config.Value;
            this.logger = logger;
        }

        public async Task EnviarTemplateAsync(EmailTemplateBase template)
        {
            try
            {
                using var smtp = new SmtpClient(config.Host, config.Port)
                {
                    Credentials = new NetworkCredential(config.User, config.Password),
                    EnableSsl = config.EnableSsl,
                    DeliveryMethod = SmtpDeliveryMethod.Network
                };

                using var mail = new MailMessage
                {
                    From = new MailAddress(config.From, config.FromName),
                    Subject = template.Assunto,
                    Body = template.GerarCorpo(),
                    IsBodyHtml = config.IsBodyHtml
                };
                mail.To.Add(template.Destinatario);

                await smtp.SendMailAsync(mail);
                logger.LogInformation("E-mail enviado para {Destinatario}", template.Destinatario);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao enviar e-mail para {Destinatario}", template.Destinatario);
                throw;
            }
        }


    }
}
