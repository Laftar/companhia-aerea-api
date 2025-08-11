using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.ViewModels.Exemplos
{
    public class ComprarPassagemExemplo : IExamplesProvider<ComprarPassagemViewModel>
    {
        public ComprarPassagemViewModel GetExamples()
        {
            return new ComprarPassagemViewModel
            {
                VooId = Guid.Parse("2a248441-0f6b-f011-8bf7-e4a8dff3558a"),
                PassageiroId = Guid.Parse("a05cacbc-4572-f011-8bf9-e4a8dff3558a")
            };
        }
    }
}
