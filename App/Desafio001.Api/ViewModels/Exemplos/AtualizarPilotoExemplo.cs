using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.ViewModels.Exemplos
{
    public class AtualizarPilotoExemplo : IExamplesProvider<AtualizarPilotoViewModel>
    {
        public AtualizarPilotoViewModel GetExamples()
        {
            return new AtualizarPilotoViewModel
            {
                Nome = "Piloto Iniciante",
                DataNascimento = DateOnly.Parse("2000-01-01"),
                QtdVoosRealizados = 0
            };
        }
    }
}
