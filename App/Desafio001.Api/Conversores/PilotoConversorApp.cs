using Desafio001.Api.ViewModels;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Api.Conversores
{
    public class PilotoConversorApp : IPilotoConversorApp
    {
        public CriarPilotoContrato ConverterContrato(CriarPilotoViewModel vm)
        {
            return new CriarPilotoContrato(
                vm.Nome,
                vm.Documento,
                vm.DataNascimento,
                vm.QtdVoosRealizados
                );
        }
        public AtualizarPilotoContrato ConverterContrato( AtualizarPilotoViewModel vm)
        {
            return new AtualizarPilotoContrato(
                vm.Nome,
                vm.DataNascimento,
                vm.QtdVoosRealizados);
        }
        public ListarPilotosContrato ConverterContrato(ListarPilotosViewModel vm)
        {
            return new ListarPilotosContrato(
                vm.Nome,
                vm.Documento);
        }
        public List<PilotoSalvoViewModel> ConverterViewModel(List<PilotoSalvoContrato> ctt)
        {
            return ctt.Select(c =>  new PilotoSalvoViewModel
            {
                Id = c.Id,
                Nome = c.Nome,
                Documento = c.Documento,
                DataNascimento = c.DataNascimento,
                QtdVoosRealizados = c.QtdVoosRealizados
            }).ToList();
        }
        public PilotoSalvoViewModel ConverterViewModel(PilotoSalvoContrato ctt)
        {
            return new PilotoSalvoViewModel
            {
                Id = ctt.Id,
                Nome = ctt.Nome,
                Documento = ctt.Documento,
                DataNascimento = ctt.DataNascimento,
                QtdVoosRealizados = ctt.QtdVoosRealizados
            };
        }
    }
}
