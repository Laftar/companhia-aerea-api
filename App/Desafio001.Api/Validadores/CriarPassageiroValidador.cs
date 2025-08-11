using Desafio001.Api.ViewModels;
using FluentValidation;

namespace Desafio001.Api.Validadores
{
    public class CriarPassageiroValidador : AbstractValidator<CriarPassageiroViewModel>
    {
        public CriarPassageiroValidador()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome é obrigatório.")
                .MinimumLength(3).WithMessage("O nome deve ter pelo menos 3 caracteres.")
                .MaximumLength(150).WithMessage("O nome pode ter no máximo 150 caracteres.");

            RuleFor(x => x.Documento)
                .NotEmpty().WithMessage("O documento é obrigatório.")
                .Matches(@"^(\d{11}|\d{3}\.\d{3}\.\d{3}\-\d{2})$").WithMessage("O documento deve um CPF e estar no formato 00000000000 ou 000.000.000-00.");


            RuleFor(x => x.DataNascimento)
                .Must(data => data <= DateOnly.FromDateTime(DateTime.Today))
                .WithMessage("A data de nascimento não pode ser no futuro.");

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));
        }
    }
}
