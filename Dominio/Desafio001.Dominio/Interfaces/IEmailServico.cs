using Desafio001.Dominio.Templates.Base;

namespace Desafio001.Servico.Servicos
{
    public interface IEmailServico
    {
        Task EnviarTemplateAsync(EmailTemplateBase template);
    }
}
