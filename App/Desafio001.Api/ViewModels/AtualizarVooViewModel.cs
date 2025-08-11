namespace Desafio001.Api.ViewModels
{
    public sealed record AtualizarVooViewModel
    {
        public DateOnly DataVoo { get; set; }
        public TimeOnly HorarioPrevSaida { get; set; }
        public TimeOnly HorarioPrevChegada { get; set; }
    }
}
