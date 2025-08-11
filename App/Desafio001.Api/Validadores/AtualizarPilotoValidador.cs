using Desafio001.Api.ViewModels;
using FluentValidation;

namespace Desafio001.Api.Validadores
{
    public class AtualizarPilotoValidador : AbstractValidator<AtualizarPilotoViewModel>
    {
        public AtualizarPilotoValidador()
        {
            RuleFor(x => x)
                .Must(x =>
                    !string.IsNullOrWhiteSpace(x.Nome) ||
                    x.DataNascimento != null ||
                    x.QtdVoosRealizados != null
                )
                .WithMessage("Ao menos um campo deve ser preenchido.");

            RuleFor(x => x.Nome)
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(150).WithMessage("O nome pode ter no máximo 150 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Nome));

            RuleFor(x => x.DataNascimento)
                .Must(data => data <= DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("A data de nascimento não pode ser no futuro.")
                .When(x => x.DataNascimento != null);

            RuleFor(x => x.QtdVoosRealizados)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade de voos realizados não pode ser negativa.")
                .When(x => x.QtdVoosRealizados != null);

        }
    }
}
