using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.ViewModels.Exemplos
{
    public class AtualizarVooExemplo : IExamplesProvider<AtualizarVooViewModel>
    {
        public AtualizarVooViewModel GetExamples()
        {
            return new AtualizarVooViewModel
            {
                DataVoo = DateOnly.Parse("2000-01-01"),
                HorarioPrevSaida = TimeOnly.Parse("10:00:00"),
                HorarioPrevChegada = TimeOnly.Parse("12:00:00"),
            };
        }
    }
}
