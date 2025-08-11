using Desafio001.Api.ViewModels;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Api.Conversores
{
    public interface IAviaoConversorApp
    {
        CriarAviaoContrato ConverterContrato(CriarAviaoViewModel vm);
        AtualizarAviaoContrato ConverterContrato(AtualizarAviaoViewModel vm);
        ListarAvioesContrato ConverterContrato(ListarAvioesViewModel vm);

        List<AviaoSalvoViewModel> ConverterViewModel(List<AviaoSalvoContrato> ctt);
        AviaoSalvoViewModel ConverterViewModel(AviaoSalvoContrato ctt);

    }
}
