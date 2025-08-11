namespace Desafio001.Api.ViewModels
{
    public sealed record VooSalvoViewModel
    {
        public Guid Id { get; set; }
        public string Status { get; set; } = string.Empty;

        public Guid AviaoId { get; set; }
        public string AviaoMarca { get; set; } = string.Empty;
        public string AviaoModelo { get; set; } = string.Empty;
        public int AviaoAnoFabricacao { get; set; }

        public Guid PilotoId { get; set; }
        public string PilotoNome { get; set; } = string.Empty;
        public DateOnly PilotoDataNascimento { get; set; }

        public DateOnly DataVoo { get; set; }
        public TimeOnly HorarioPrevSaida { get; set; }
        public TimeOnly HorarioPrevChegada { get; set; }
        public TimeOnly? HorarioRealSaida { get; set; }
        public TimeOnly? HorarioRealChegada { get; set; }

        public int QtdPassagensVendidas { get; set; }
        public int QtdPassageirosPresentes { get; set; }
    }
}
