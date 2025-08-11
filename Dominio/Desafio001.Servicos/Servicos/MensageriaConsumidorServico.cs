using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Entidades.Enums;
using Desafio001.Dominio.Templates;
using Microsoft.Extensions.Logging;
using Desafio001.Dominio.Interfaces;
using Microsoft.EntityFrameworkCore;
using Desafio001.Dominio.Entidades;

namespace Desafio001.Servico.Servicos
{
    public class MensageriaConsumidorServico(
        ILogger<MensageriaConsumidorServico> logger,
        IEmailServico emailServico,
        IRepositorioBase<VooPassageiro> vooPassageiroRep) : IMensageriaConsumidorServico
    {

        private readonly ILogger<MensageriaConsumidorServico> logger = logger;
        private readonly IEmailServico emailServico = emailServico;
        private readonly IRepositorioBase<VooPassageiro> vooPassageiroRep = vooPassageiroRep;

        public async Task NotificarCancelamentoVooAsync(NotificarCancelamentoVooContrato contrato, CancellationToken ct)
        {
            try
            {
                var passageiros = await vooPassageiroRep
                    .MontarConsulta()
                    .Where(x => x.VooId == contrato.VooId
                             && x.Status != VooPassageiroStatus.Cancelado
                             && !x.Excluido
                             && !string.IsNullOrEmpty(x.Passageiro.Email))
                    .Select(x => x.Passageiro)
                    .ToListAsync(ct);

                foreach (var passageiro in passageiros)
                {
                    try
                    {
                        var template = new EmailCancelamentoVoo(passageiro.Email)
                        {
                            NomePassageiro = passageiro.Nome,
                            VooId = contrato.VooId
                        };

                        await emailServico.EnviarTemplateAsync(template);

                        logger.LogWarning("Passageiro {PassageiroId} notificada do cancelamento do voo", passageiro.Id);
                    }
                    catch
                    {
                        logger.LogWarning("Passageiro {PassageiroId} não foi possivel notificar do cancelamento do voo", passageiro.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao processar notificação de overbooking");
            }
        }

        public async Task NotificarOverbookingVooAsync(NotificarOverbookingVooContrato contrato, CancellationToken ct)
        {
            try
            {
                var passageiros = await vooPassageiroRep
                    .MontarConsulta()
                    .Where(x => x.VooId == contrato.VooId
                             && x.Status == VooPassageiroStatus.Pendente
                             && !x.Excluido
                             && !string.IsNullOrEmpty(x.Passageiro.Email))
                    .Select(x => x.Passageiro)
                    .ToListAsync(ct);

                foreach (var passageiro in passageiros)
                {
                    try
                    {   
                        var template = new EmailOverbookingVoo(passageiro.Email)
                        {
                            NomePassageiro = passageiro.Nome,
                            VooId = contrato.VooId
                        };

                        await emailServico.EnviarTemplateAsync(template);

                        logger.LogWarning("Passageiro {PassageiroId} notificada do overbooking", passageiro.Id);
                    }
                    catch
                    {
                        logger.LogWarning("Passageiro {PassageiroId} não foi possivel notificar overbooking", passageiro.Id);
                    }
                }
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Erro ao processar notificação de overbooking");
            }
        }

        
    }
}
