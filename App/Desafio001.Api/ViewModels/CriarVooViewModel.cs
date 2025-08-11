namespace Desafio001.Api.ViewModels
{
    public sealed record CriarVooViewModel
    {
        public Guid AviaoId { get; set; }
        public Guid PilotoId { get; set; }
        public DateOnly DataVoo { get; set; }
        public TimeOnly HorarioPrevSaida { get; set; }
        public TimeOnly HorarioPrevChegada { get; set; }
    }
}
