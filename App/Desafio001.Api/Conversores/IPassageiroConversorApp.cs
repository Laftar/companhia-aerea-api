using Desafio001.Api.ViewModels;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Api.Conversores
{
    public interface IPassageiroConversorApp
    {
        CriarPassageiroContrato ConverterContrato(CriarPassageiroViewModel vm);
        AtualizarPassageiroContrato ConverterContrato(AtualizarPassageiroViewModel vm);
        ListarPassageirosContrato ConverterContrato(ListarPassageirosViewModel vm);

        List<PassageiroSalvoViewModel> ConverterViewModel(List<PassageiroSalvoContrato> ctt);
        PassageiroSalvoViewModel ConverterViewModel(PassageiroSalvoContrato ctt);

    }
}
