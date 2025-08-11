using Desafio001.Api.ViewModels;
using FluentValidation;

namespace Desafio001.Api.Validadores
{
    public class CriarAviaoValidador : AbstractValidator<CriarAviaoViewModel>
    {
        public CriarAviaoValidador()
        {
            RuleFor(x => x.Marca)
                .NotEmpty().WithMessage("A marca é obrigatória.")
                .MaximumLength(50).WithMessage("A marca deve ter no máximo 50 caracteres.");

            RuleFor(x => x.Modelo)
                .NotEmpty().WithMessage("O modelo é obrigatório.")
                .MaximumLength(50).WithMessage("O modelo deve ter no máximo 50 caracteres.");

            RuleFor(x => x.AnoFabricacao)
                .LessThanOrEqualTo(DateTime.Now.Year).WithMessage("O ano de fabricação não pode ser positivo.");

            RuleFor(x => x.QtdMaxPassageiros)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade máxima de passageiros não pode ser negativa.");

            RuleFor(x => x.QtdVoosRealizados)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade de voos realizados não pode ser negativa.");
        }
    }
}
