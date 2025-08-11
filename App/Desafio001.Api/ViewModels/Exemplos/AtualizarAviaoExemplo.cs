using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.ViewModels.Exemplos
{
    public class AtualizarAviaoExemplo : IExamplesProvider<AtualizarAviaoViewModel>
    {
        public AtualizarAviaoViewModel GetExamples()
        {
            return new AtualizarAviaoViewModel
            {
                Modelo = "Airbus",
                AnoFabricacao = 2010
            };
        }
    }
}
