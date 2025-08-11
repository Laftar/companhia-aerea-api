using Desafio001.Api.ViewModels;
using FluentValidation;

namespace Desafio001.Api.Validadores
{
    public class AtualizarPassageiroValidador : AbstractValidator<AtualizarPassageiroViewModel>
    {
        public AtualizarPassageiroValidador()
        {
            RuleFor(x => x)
                .Must(x =>
                    !string.IsNullOrWhiteSpace(x.Nome) ||
                    x.DataNascimento != null ||
                    !string.IsNullOrWhiteSpace(x.Email)
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

            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Stop)
                .NotEmpty().WithMessage("O e-mail é obrigatório.")
                .EmailAddress().WithMessage("Formato de e-mail inválido.")
                .When(x => !string.IsNullOrWhiteSpace(x.Email));
        }
    }
}
