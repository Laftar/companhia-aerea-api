namespace Desafio001.Api.ViewModels
{
    public sealed record AtualizarAviaoViewModel
    {
        public string? Modelo { get; set; }
        public int? AnoFabricacao { get; set; }
    }
}
