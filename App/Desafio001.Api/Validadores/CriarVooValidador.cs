using Desafio001.Api.ViewModels;
using FluentValidation;

namespace Desafio001.Api.Validadores
{
    public class CriarVooValidador : AbstractValidator<CriarVooViewModel>
    {
        public CriarVooValidador()
        {
            RuleFor(x => x.AviaoId)
                .NotEmpty().WithMessage("O avião é obrigatório.");

            RuleFor(x => x.PilotoId)
                .NotEmpty().WithMessage("O piloto é obrigatório.");

            RuleFor(x => x.DataVoo)
                .Must(data =>
                {
                    var hoje = DateOnly.FromDateTime(DateTime.Now.Date);
                    return data >= hoje;
                })
                .WithMessage("A data do voo deve ser hoje ou uma data futura.");

            RuleFor(x => x.HorarioPrevSaida)
                .NotNull().WithMessage("O horário de saída é obrigatório.");

            RuleFor(x => x.HorarioPrevChegada)
                .NotNull().WithMessage("O horário de chegada é obrigatório.");

            RuleFor(x => x)
                .Must(x => x.HorarioPrevChegada > x.HorarioPrevSaida)
                .WithMessage("O horário de chegada deve ser depois do horário de saída.");

            RuleFor(x => x)
                .Must(x =>
                {
                    var agora = DateTime.Now;
                    var dataHoje = DateOnly.FromDateTime(agora);
                    var horarioAtual = TimeOnly.FromDateTime(agora);

                    if (x.DataVoo == dataHoje)
                    {
                        return x.HorarioPrevSaida > horarioAtual && x.HorarioPrevChegada > horarioAtual;
                    }

                    return true;
                })
                .WithMessage("Os horários devem ser futuros em relação ao momento atual.");

        }
    }
}
