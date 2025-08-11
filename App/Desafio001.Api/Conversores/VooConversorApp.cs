using Desafio001.Api.ViewModels;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Api.Conversores
{
    public class VooConversorApp : IVooConversorApp
    {
        public CriarVooContrato ConverterContrato(CriarVooViewModel vm)
        {
            return new CriarVooContrato(
                vm.AviaoId,
                vm.PilotoId,
                vm.DataVoo,
                vm.HorarioPrevSaida,
                vm.HorarioPrevChegada
            );
        }
        public AtualizarVooContrato ConverterContrato(AtualizarVooViewModel vm)
        {
            return new AtualizarVooContrato(
                vm.DataVoo,
                vm.HorarioPrevSaida,
                vm.HorarioPrevChegada);
        }
        public ConcluirVooContrato ConverterContrato(ConcluirVooViewModel vm)
        {
            return new ConcluirVooContrato(
                vm.HorarioRealSaida,
                vm.HorarioRealChegada
            );
        }
        public ListarVoosContrato ConverterContrato(ListarVoosViewModel vm)
        {
            return new ListarVoosContrato(
                vm.DataVoo,
                vm.AviaoId,
                vm.PilotoId);
        }

        public ListarPassagensContrato ConverterContrato(ListarPassagensViewModel vm)
        {
            return new ListarPassagensContrato(
                VooId: vm.VooId,
                PassageiroId: vm.PassageiroId,
                Status: vm.Status);
        }
        public List<VooSalvoViewModel> ConverterViewModel(List<VooSalvoContrato> ctt)
        {
            return ctt.Select(c => new VooSalvoViewModel
            {
                Id = c.Id,
                Status = c.Status.ToString(),

                AviaoId = c.AviaoId,
                AviaoMarca = c.AviaoMarca,
                AviaoModelo = c.AviaoModelo,
                AviaoAnoFabricacao = c.AviaoAnoFabricacao,

                PilotoId = c.PilotoId,
                PilotoNome = c.PilotoNome,
                PilotoDataNascimento = c.PilotoDataNascimento,

                DataVoo = c.DataVoo,
                HorarioPrevSaida = c.HorarioPrevSaida,
                HorarioPrevChegada = c.HorarioPrevChegada,
                HorarioRealSaida = c.HorarioRealSaida,
                HorarioRealChegada = c.HorarioRealChegada,

                QtdPassagensVendidas = c.QtdPassagensVendidas,
                QtdPassageirosPresentes = c.QtdPassageirosPresentes
            }).ToList();
        }
        public VooSalvoViewModel ConverterViewModel(VooSalvoContrato ctt)
        {
            return new VooSalvoViewModel
            {
                Id = ctt.Id,
                Status = ctt.Status.ToString(),

                AviaoId = ctt.AviaoId,
                AviaoMarca = ctt.AviaoMarca,
                AviaoModelo = ctt.AviaoModelo,
                AviaoAnoFabricacao = ctt.AviaoAnoFabricacao,

                PilotoId = ctt.PilotoId,
                PilotoNome = ctt.PilotoNome,
                PilotoDataNascimento = ctt.PilotoDataNascimento,

                DataVoo = ctt.DataVoo,
                HorarioPrevSaida = ctt.HorarioPrevSaida,
                HorarioPrevChegada = ctt.HorarioPrevChegada,
                HorarioRealSaida = ctt.HorarioRealSaida,
                HorarioRealChegada = ctt.HorarioRealChegada,

                QtdPassagensVendidas = ctt.QtdPassagensVendidas,
                QtdPassageirosPresentes = ctt.QtdPassageirosPresentes
            };
        }


        public List<PassagemSalvaViewModel> ConverterViewModel(List<PassagemSalvaContrato> ctt)
        {
            return ctt.Select(c => new PassagemSalvaViewModel
            {
                Id = c.Id,
                PassagemStatus = c.PassagemStatus,

                PassageiroId = c.PassageiroId,
                Nome = c.Nome,
                Documento = c.Documento,
                DataNascimento = c.DataNascimento,

                VooId = c.VooId,
                DataVoo = c.DataVoo,
                VooStatus = c.VooStatus
            }).ToList();
        }
        public ComprarPassagemContrato ConverterContrato(ComprarPassagemViewModel vm)
        {
            return new ComprarPassagemContrato(
                vm.PassageiroId,
                vm.VooId
            );
        }
        public PassagemSalvaViewModel ConverterViewModel(PassagemSalvaContrato ctt)
        {
            return new PassagemSalvaViewModel
            {
                Id = ctt.Id,
                PassagemStatus = ctt.PassagemStatus,

                PassageiroId = ctt.PassageiroId,
                Nome = ctt.Nome,
                Documento = ctt.Documento,
                DataNascimento = ctt.DataNascimento,

                VooId = ctt.VooId,
                DataVoo = ctt.DataVoo,
                VooStatus = ctt.VooStatus
            };
        }
    }
}
