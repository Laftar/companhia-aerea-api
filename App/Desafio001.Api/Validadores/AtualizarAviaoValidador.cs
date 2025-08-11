using Desafio001.Api.ViewModels;
using FluentValidation;

namespace Desafio001.Api.Validadores
{
    public class AtualizarAviaoValidador : AbstractValidator<AtualizarAviaoViewModel>
    {
        public AtualizarAviaoValidador()
        {
            RuleFor(x => x)
                .Must(x =>
                    !string.IsNullOrWhiteSpace(x.Modelo) ||
                    x.AnoFabricacao != null
                )
                .WithMessage("Ao menos um campo deve ser preenchido.");

            RuleFor(x => x.Modelo)
                .MaximumLength(50).WithMessage("O modelo deve ter no máximo 50 caracteres.")
                .When(x => !string.IsNullOrWhiteSpace(x.Modelo));

            RuleFor(x => x.AnoFabricacao)
                .LessThanOrEqualTo(0).WithMessage("O ano de fabricação não pode ser positivo.")
                .When(x => x.AnoFabricacao.HasValue);

        }
    }
}
