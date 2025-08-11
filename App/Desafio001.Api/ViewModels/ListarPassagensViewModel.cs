using Desafio001.Dominio.Entidades.Enums;

namespace Desafio001.Api.ViewModels
{
    public sealed record ListarPassagensViewModel
    {
        public Guid? VooId { get; set; }
        public Guid? PassageiroId { get; set; }
        public VooPassageiroStatus? Status {  get; set; }
    }
}
