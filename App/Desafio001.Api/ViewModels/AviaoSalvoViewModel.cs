namespace Desafio001.Api.ViewModels
{
    public sealed record AviaoSalvoViewModel
    {
        public Guid Id { get; set; }
        public string Marca { get; set; } = string.Empty;
        public string Modelo { get; set; } = string.Empty;
        public int AnoFabricacao { get; set; }
        public int QtdMaxPassageiros { get; set; }
        public int QtdVoosRealizados { get; set; }

    }
}
