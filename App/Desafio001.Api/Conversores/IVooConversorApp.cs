using Desafio001.Api.ViewModels;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Api.Conversores
{
    public interface IVooConversorApp
    {
        CriarVooContrato ConverterContrato(CriarVooViewModel vm);
        ConcluirVooContrato ConverterContrato(ConcluirVooViewModel vm);
        AtualizarVooContrato ConverterContrato(AtualizarVooViewModel vm);
        ListarVoosContrato ConverterContrato(ListarVoosViewModel vm);
        List<VooSalvoViewModel> ConverterViewModel(List<VooSalvoContrato> ctt);
        VooSalvoViewModel ConverterViewModel(VooSalvoContrato ctt);


        ListarPassagensContrato ConverterContrato(ListarPassagensViewModel vm);
        List<PassagemSalvaViewModel> ConverterViewModel(List<PassagemSalvaContrato> ctt);
        ComprarPassagemContrato ConverterContrato(ComprarPassagemViewModel vm);
        PassagemSalvaViewModel ConverterViewModel(PassagemSalvaContrato ctt);
    }
}
