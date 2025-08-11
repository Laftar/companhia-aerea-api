using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.ViewModels.Exemplos
{
    public class CriarPilotoExemplo : IExamplesProvider<CriarPilotoViewModel>
    {
        public CriarPilotoViewModel GetExamples()
        {
            return new CriarPilotoViewModel
            {
                Nome = "Piloto Iniciante",
                Documento = "123.456.789-00",
                DataNascimento = DateOnly.Parse("2000-01-01"),
                QtdVoosRealizados = 0
            };
        }
    }
}
