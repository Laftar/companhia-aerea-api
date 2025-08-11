using Desafio001.Api.ViewModels;
using FluentValidation;

namespace Desafio001.Api.Validadores
{
    public class ComprarPassagemValidador : AbstractValidator<ComprarPassagemViewModel>
    {
        public ComprarPassagemValidador()
        {
            RuleFor(x => x.VooId)
                .NotEmpty().WithMessage("O ID do Voo é obrigatório.");

            RuleFor(x => x.PassageiroId)
                .NotEmpty().WithMessage("O ID do passageiro é obrigatório.");

        }
    }
}
