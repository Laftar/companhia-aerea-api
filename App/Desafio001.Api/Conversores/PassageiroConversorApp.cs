using Desafio001.Api.ViewModels;
using Desafio001.Dominio.Contratos;

namespace Desafio001.Api.Conversores
{
    public class PassageiroConversorApp : IPassageiroConversorApp
    {
        public CriarPassageiroContrato ConverterContrato(CriarPassageiroViewModel vm)
        {
            return new CriarPassageiroContrato(
                vm.Nome,
                vm.Documento,
                vm.DataNascimento,
                vm.Email
                );
        }
        public AtualizarPassageiroContrato ConverterContrato( AtualizarPassageiroViewModel vm)
        {
            return new AtualizarPassageiroContrato(
                vm.Nome,
                vm.DataNascimento,
                vm.Email);
        }
        public ListarPassageirosContrato ConverterContrato( ListarPassageirosViewModel vm)
        {
            return new ListarPassageirosContrato(
                vm.Nome,
                vm.Documento);
        }


        public List<PassageiroSalvoViewModel> ConverterViewModel(List<PassageiroSalvoContrato> ctt)
        {
            return ctt.Select(c => new PassageiroSalvoViewModel{
                Id = c.Id,
                Nome = c.Nome,
                Documento = c.Documento,
                DataNascimento = c.DataNascimento,
                Email = c.Email
            }).ToList();
        }
        public PassageiroSalvoViewModel ConverterViewModel(PassageiroSalvoContrato ctt)
        {
            return new PassageiroSalvoViewModel
            {
                Id = ctt.Id,
                Nome = ctt.Nome,
                Documento = ctt.Documento,
                DataNascimento = ctt.DataNascimento,
                Email = ctt.Email
            };
        }

    }
}
