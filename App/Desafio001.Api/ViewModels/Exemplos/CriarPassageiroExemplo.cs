using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.ViewModels.Exemplos
{
    public class CriarPassageiroExemplo : IExamplesProvider<CriarPassageiroViewModel>
    {
        public CriarPassageiroViewModel GetExamples()
        {
            return new CriarPassageiroViewModel
            {
                Nome = "Passageiro Idoso",
                Documento = "123.456.789-00",
                DataNascimento = DateOnly.Parse("2000-01-01"),
                Email = "email@gmail.com.br"
            };
        }
    }
}
