using Desafio001.Api.ViewModels;
using FluentValidation;

namespace Desafio001.Api.Validadores
{
    public class CriarPilotoValidador : AbstractValidator<CriarPilotoViewModel>
    {
        public CriarPilotoValidador()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(150).WithMessage("O nome pode ter no máximo 150 caracteres.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("O documento é obrigatório.")
                .Matches(@"^(\d{11}|\d{3}\.\d{3}\.\d{3}\-\d{2})$").WithMessage("O documento deve conter um CPF válido.");

            RuleFor(x => x.DataNascimento)
                .Must(data => data <= DateOnly.FromDateTime(DateTime.Today.AddYears(-18)))
                .WithMessage("O piloto deve ter pelo menos 18 anos.");

            RuleFor(x => x.QtdVoosRealizados)
                .GreaterThanOrEqualTo(0).WithMessage("A quantidade de voos realizados não pode ser negativa.");
        }
    }
}
