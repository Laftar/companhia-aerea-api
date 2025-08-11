using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.ViewModels.Exemplos
{
    public class AtualizarPassageiroExemplo : IExamplesProvider<AtualizarPassageiroViewModel>
    {
        public AtualizarPassageiroViewModel GetExamples()
        {
            return new AtualizarPassageiroViewModel
            {
                Nome = "Passageiro Idoso",
                DataNascimento = DateOnly.Parse("2000-01-01"),
                Email = "emailnovo@gmail.com.br"
            };
        }
    }
}
