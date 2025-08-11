namespace Desafio001.Api.ViewModels
{
    public sealed record ConcluirVooViewModel
    {
        public TimeOnly HorarioRealSaida { get; set; }
        public TimeOnly HorarioRealChegada { get; set; }
    }
}
