namespace Desafio001.Api.ViewModels
{
    public sealed record ListarVoosViewModel
    {
        public DateOnly? DataVoo { get; set; }
        public Guid? AviaoId { get; set; }
        public Guid? PilotoId { get; set; }
    }
}
