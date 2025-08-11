using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.ViewModels.Exemplos
{
    public class ConcluirVooExemplo : IExamplesProvider<ConcluirVooViewModel>
    {
        public ConcluirVooViewModel GetExamples()
        {
            return new ConcluirVooViewModel
            {
                HorarioRealSaida = TimeOnly.Parse("09:00:00"),
                HorarioRealChegada = TimeOnly.Parse("11:00:00"),
            };
        }
    }
}
