using Desafio001.Api.ViewModels;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Api.Conversores
{
    public interface IPilotoConversorApp
    {
        CriarPilotoContrato ConverterContrato(CriarPilotoViewModel vm);

        AtualizarPilotoContrato ConverterContrato(AtualizarPilotoViewModel vm);

        ListarPilotosContrato ConverterContrato(ListarPilotosViewModel vm);
        List<PilotoSalvoViewModel> ConverterViewModel(List<PilotoSalvoContrato> ctt);

        PilotoSalvoViewModel ConverterViewModel(PilotoSalvoContrato ctt);
    }
}
