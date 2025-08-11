using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Entidades;
using Desafio001.Dominio.Entidades.Enums;
using Desafio001.Dominio.Exceptions;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Mensagens;
using Desafio001.Servico.Conversores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Desafio001.Servico.Servicos
{
    public class VooServico(
        IRepositorioBase<Voo> vooRep,
        IRepositorioBase<Piloto> pilotoRep,
        IRepositorioBase<Aviao> aviaoRep,
        IVooConversor vooConv,
        ILogger<VooServico> logger,
        IMensageriaProdutorServico mensageria) : IVooServico
    {
        private readonly IRepositorioBase<Voo> vooRep = vooRep;
        private readonly IRepositorioBase<Piloto> pilotoRep = pilotoRep;
        private readonly IRepositorioBase<Aviao> aviaoRep = aviaoRep;
        private readonly IVooConversor vooConv = vooConv;
        private readonly ILogger<VooServico> logger = logger;
        private readonly IMensageriaProdutorServico mensageria = mensageria;

        public async Task<VooSalvoContrato> BuscarVooAsync(Guid id, CancellationToken ct)
        {
            var voo = await vooRep.ObterPorIdAsync(id, ct);
            if (voo is null)
                throw new CustomException("{VooId} Voo não encontrado");
            else if (voo.Excluido)
                throw new CustomException("{VooExcluido} Voo deletado");

            return new VooSalvoContrato(
                voo.Id,
                voo.Status,
                voo.AviaoId,
                voo.Aviao.Marca,
                voo.Aviao.Modelo,
                voo.Aviao.AnoFabricacao,
                voo.PilotoId,
                voo.Piloto.Nome,
                voo.Piloto.DataNascimento,
                voo.DataVoo,
                voo.HorarioPrevSaida,
                voo.HorarioPrevChegada,
                voo.HorarioRealSaida,
                voo.HorarioRealChegada,
                voo.QtdPassagensVendidas,
                voo.QtdPassageirosPresentes
            );
        }
        public async Task<List<VooSalvoContrato>> ListarVoosAsync(ListarVoosContrato contrato, CancellationToken ct)
        {

            var voos = await vooRep.MontarConsulta()
                .Where(v => !v.Excluido
                    && (!contrato.DataVoo.HasValue || v.DataVoo == contrato.DataVoo)
                    && (!contrato.AviaoId.HasValue || v.AviaoId == contrato.AviaoId)
                    && (!contrato.PilotoId.HasValue || v.PilotoId == contrato.PilotoId))
                .Select(v => new VooSalvoContrato(
                    v.Id,
                    v.Status,

                    v.AviaoId,
                    v.Aviao.Marca,
                    v.Aviao.Modelo,
                    v.Aviao.AnoFabricacao,

                    v.PilotoId,
                    v.Piloto.Nome,
                    v.Piloto.DataNascimento,

                    v.DataVoo,
                    v.HorarioPrevSaida,
                    v.HorarioPrevChegada,
                    v.HorarioRealSaida,
                    v.HorarioRealChegada,

                    v.QtdPassagensVendidas,
                    v.QtdPassageirosPresentes)
                ).ToListAsync(ct);
            return voos;
        }
        public async Task<VooSalvoContrato> CriarVooAsync(CriarVooContrato contrato, CancellationToken ct)
        {
            var piloto = await pilotoRep.ObterPorIdAsync(contrato.PilotoId, ct);
            if (piloto is null)
                throw new CustomException("{PilotoId} Entidade não encontrada");
            else if (piloto.Excluido)
                throw new CustomException("{PilotoExcluido} Entidade esta excluida");

            var aviao = await aviaoRep.ObterPorIdAsync(contrato.AviaoId, ct);
            if (aviao is null)
                throw new CustomException("{AviaoId} Entidade não encontrada");
            else if (aviao.Excluido)
                throw new CustomException("{AviaoExcluido} Entidade está excluída");

            var voo = vooConv.ConverterParaEntidade(contrato);
            var salvo = await vooRep.AdicionarAsync(voo, ct);
            if (salvo.Id == Guid.Empty)
                throw new CustomException("Não foi possivel concluir o procedimento");

            return new VooSalvoContrato(
                salvo.Id,
                salvo.Status,
                salvo.AviaoId,
                salvo.Aviao.Marca,
                salvo.Aviao.Modelo,
                salvo.Aviao.AnoFabricacao,
                salvo.PilotoId,
                salvo.Piloto.Nome,
                salvo.Piloto.DataNascimento,
                salvo.DataVoo,
                salvo.HorarioPrevSaida,
                salvo.HorarioPrevChegada,
                salvo.HorarioRealSaida,
                salvo.HorarioRealChegada,
                salvo.QtdPassagensVendidas,
                salvo.QtdPassageirosPresentes
            );

        }
        public async Task<VooSalvoContrato> AtualizarVooAsync(Guid id, AtualizarVooContrato contrato, CancellationToken ct)
        {
            var voo = await vooRep.ObterPorIdAsync(id, ct);
            if (voo is null)
                throw new CustomException("{VooId} Entidade não encontrada");
            else if (voo.Excluido)
                throw new CustomException("{VooExcluido} Voo esta excluido.");
            else if (voo.Status != VooStatus.Pendente)
                throw new CustomException("{VooStatus} Status do voo não permite");

            voo.AtualizarVoo(
                contrato.DataVoo,
                contrato.HorarioPrevSaida,
                contrato.HorarioPrevChegada);

            var salvo = await vooRep.AtualizarAsync(voo, ct);

            return new VooSalvoContrato(
                salvo.Id,
                salvo.Status,
                salvo.AviaoId,
                salvo.Aviao.Marca,
                salvo.Aviao.Modelo,
                salvo.Aviao.AnoFabricacao,
                salvo.PilotoId,
                salvo.Piloto.Nome,
                salvo.Piloto.DataNascimento,
                salvo.DataVoo,
                salvo.HorarioPrevSaida,
                salvo.HorarioPrevChegada,
                salvo.HorarioRealSaida,
                salvo.HorarioRealChegada,
                salvo.QtdPassagensVendidas,
                salvo.QtdPassageirosPresentes
            );
        }
        public async Task<VooSalvoContrato> ConcluirVooAsync(Guid id, ConcluirVooContrato contrato, CancellationToken ct)
        {
            var voo = await vooRep.ObterPorIdAsync(id, ct);
            if (voo is null)
                throw new CustomException("{VooId} Entidade não encontrada");
            else if (voo.Excluido)
                throw new CustomException("{VooExcluido} Voo esta excluido.");
            else if (voo.Status != VooStatus.Pendente)
                throw new CustomException("{VooStatus} Status do voo não permite");

            voo.ConcluirVoo(
                contrato.HorarioRealSaida,
                contrato.HorarioRealChegada);

            voo.Aviao.VooRealizado();
            voo.Piloto.VooRealizado();

            var salvo = await vooRep.AtualizarAsync(voo, ct);

            return new VooSalvoContrato(
                salvo.Id,
                salvo.Status,
                salvo.AviaoId,
                salvo.Aviao.Marca,
                salvo.Aviao.Modelo,
                salvo.Aviao.AnoFabricacao,
                salvo.PilotoId,
                salvo.Piloto.Nome,
                salvo.Piloto.DataNascimento,
                salvo.DataVoo,
                salvo.HorarioPrevSaida,
                salvo.HorarioPrevChegada,
                salvo.HorarioRealSaida,
                salvo.HorarioRealChegada,
                salvo.QtdPassagensVendidas,
                salvo.QtdPassageirosPresentes
            );
        }
        public async Task<Guid> DeletarVooAsync(Guid id, CancellationToken ct)
        {
            var voo = await vooRep.ObterPorIdAsync(id, ct);
            if (voo is null)
                throw new CustomException("{VooId} Entidade não encontrada");
            else if (voo.Excluido)
                throw new CustomException("{VooExcluido} Voo já esta excluido");
            else if (voo.Status is VooStatus.Vooando or VooStatus.Concluido)
                throw new CustomException("{VooStatus} Status não permite essa operação");

            voo.Excluir();
            await mensageria.PublicarMensagemAsync(
                    new CancelamentoVooMensagem(voo.Id),
                    ct);

            var salvo = await vooRep.AtualizarAsync(voo, ct);
            return salvo.Id;
        }
    }


}
