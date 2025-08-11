using Desafio001.Api.ViewModels;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Api.Conversores
{
    public class AviaoConversorApp : IAviaoConversorApp
    {
        public CriarAviaoContrato ConverterContrato(CriarAviaoViewModel vm)
        {
            return new CriarAviaoContrato(
                vm.Marca,
                vm.Modelo,
                vm.AnoFabricacao,
                vm.QtdMaxPassageiros,
                vm.QtdVoosRealizados
                );
        }
        public AtualizarAviaoContrato ConverterContrato( AtualizarAviaoViewModel vm)
        {
            return new AtualizarAviaoContrato(
                vm.Modelo,
                vm.AnoFabricacao);
        }
        public ListarAvioesContrato ConverterContrato( ListarAvioesViewModel vm)
        {
            return new ListarAvioesContrato(
                vm.Marca,
                vm.Modelo);
        }

        public List<AviaoSalvoViewModel> ConverterViewModel(List<AviaoSalvoContrato> ctt)
        {
            return ctt.Select(c => new AviaoSalvoViewModel{
                Id = c.Id,
                Marca = c.Marca,
                Modelo = c.Modelo,
                AnoFabricacao = c.AnoFabricacao,
                QtdMaxPassageiros = c.QtdMaxPassageiros,
                QtdVoosRealizados = c.QtdVoosRealizados
            }).ToList();
        }
        public AviaoSalvoViewModel ConverterViewModel(AviaoSalvoContrato ctt)
        {
            return new AviaoSalvoViewModel
            {
                Id = ctt.Id,
                Marca = ctt.Marca,
                Modelo = ctt.Modelo,
                AnoFabricacao = ctt.AnoFabricacao,
                QtdMaxPassageiros = ctt.QtdMaxPassageiros,
                QtdVoosRealizados = ctt.QtdVoosRealizados
            };
        }

    }
}
