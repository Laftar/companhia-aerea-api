using Desafio001.Dominio.Entidades.Enums;

namespace Desafio001.Dominio.Contratos
{
    public sealed record ListarPassagensContrato (
        Guid? VooId,
        Guid? PassageiroId,
        VooPassageiroStatus? Status);
}
