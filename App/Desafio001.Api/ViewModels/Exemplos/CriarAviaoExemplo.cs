using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.ViewModels.Exemplos
{
    public class CriarAviaoExemplo : IExamplesProvider<CriarAviaoViewModel>
    {
        public CriarAviaoViewModel GetExamples()
        {
            return new CriarAviaoViewModel
            {
                Marca = "Boeing",
                Modelo = "737-800",
                AnoFabricacao = 2015,
                QtdMaxPassageiros = 100,
                QtdVoosRealizados = 0
            };
        }
    }
}
