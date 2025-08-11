namespace Desafio001.Dominio.Contratos
{
    public sealed record ComprarPassagemContrato(
        Guid PassageiroId,
        Guid VooId
    );
}
