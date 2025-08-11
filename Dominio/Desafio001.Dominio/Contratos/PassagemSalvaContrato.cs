using Desafio001.Dominio.Entidades.Enums;

namespace Desafio001.Dominio.Contratos
{
    public sealed record PassagemSalvaContrato(
        Guid Id,
        VooPassageiroStatus PassagemStatus,

        Guid PassageiroId,
        string Nome,
        string Documento,
        DateOnly DataNascimento,

        Guid VooId,
        DateOnly DataVoo,
        VooStatus VooStatus
    );
}

