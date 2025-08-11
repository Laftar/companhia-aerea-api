using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.ViewModels.Exemplos
{
    public class CriarVooExemplo : IExamplesProvider<CriarVooViewModel>
    {
        public CriarVooViewModel GetExamples()
        {
            return new CriarVooViewModel
            {
                AviaoId = Guid.Parse("e8b49301-0e6b-f011-8bf7-e4a8dff3558a"),
                PilotoId = Guid.Parse("1c21edd7-0e6b-f011-8bf7-e4a8dff3558a"),
                DataVoo = DateOnly.Parse("2025-08-10"),
                HorarioPrevSaida = TimeOnly.Parse("08:00:00"),
                HorarioPrevChegada = TimeOnly.Parse("10:00:00"),
            };
        }
    }
}
