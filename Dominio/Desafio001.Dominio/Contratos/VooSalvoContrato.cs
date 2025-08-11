using Desafio001.Dominio.Entidades.Enums;

namespace Desafio001.Dominio.Contratos
{
    public sealed record VooSalvoContrato(
        Guid Id,
        VooStatus Status,

        Guid AviaoId,
        string AviaoMarca,
        string AviaoModelo,
        int AviaoAnoFabricacao,

        Guid PilotoId,
        string PilotoNome,
        DateOnly PilotoDataNascimento,

        DateOnly DataVoo,
        TimeOnly HorarioPrevSaida,
        TimeOnly HorarioPrevChegada,
        TimeOnly? HorarioRealSaida,
        TimeOnly? HorarioRealChegada,

        int QtdPassagensVendidas,
        int QtdPassageirosPresentes
    );
}
