namespace Desafio001.Api.ViewModels
{
    public sealed record ComprarPassagemViewModel
    {
        public Guid PassageiroId { get; set; }
        public Guid VooId { get; set; }
    }
}
