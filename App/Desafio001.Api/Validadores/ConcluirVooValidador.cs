using Desafio001.Api.ViewModels;
using FluentValidation;

namespace Desafio001.Api.Validadores
{
    public class ConcluirVooValidador : AbstractValidator<ConcluirVooViewModel>
    {
        public ConcluirVooValidador()
        {
            RuleFor(x => x.HorarioRealChegada)
                .NotNull().WithMessage("O horário de chegada é obrigatório.");

            RuleFor(x => x)
                .Must(x => x.HorarioRealChegada > x.HorarioRealSaida)
                .WithMessage("O horário de chegada deve ser depois do horário de saída.");

            RuleFor(x => x)
                .Must(x => x.HorarioRealChegada > x.HorarioRealSaida)
                .WithMessage("O horário de chegada deve ser depois do horário de saída.");
        }
    }
}
