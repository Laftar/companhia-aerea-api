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
    public class VooPassageiroServico(
        IVooPassageiroConversor vooPassageiroConv,
        IRepositorioBase<VooPassageiro> vooPassageiroRep,
        IRepositorioBase<Passageiro> passageiroRep,
        IRepositorioBase<Voo> vooRep,
        IMensageriaProdutorServico mensageria,
        ILogger<VooPassageiroServico> logger) : IVooPassageiroServico
    {
        private readonly IVooPassageiroConversor vooPassageiroConv = vooPassageiroConv;
        private readonly IRepositorioBase<VooPassageiro> vooPassageiroRep = vooPassageiroRep;
        private readonly IRepositorioBase<Passageiro> passageiroRep = passageiroRep;
        private readonly IRepositorioBase<Voo> vooRep = vooRep;
        private readonly IMensageriaProdutorServico mensageria = mensageria;
        private readonly ILogger<VooPassageiroServico> logger = logger;

        public async Task<List<PassagemSalvaContrato>> ListarPassagensAsync(ListarPassagensContrato contrato, CancellationToken ct)
        {
            var vooPassageiro = await vooPassageiroRep.MontarConsulta()
            .Where(v => !v.Excluido
                && (!contrato.VooId.HasValue || v.VooId == contrato.VooId)
                && (!contrato.PassageiroId.HasValue || v.PassageiroId == contrato.PassageiroId)
                && (!contrato.Status.HasValue || v.Status == contrato.Status))
            .Select(p => new PassagemSalvaContrato(
                p.Id,
                p.Status,

                p.Passageiro.Id,
                p.Passageiro.Nome,
                p.Passageiro.Documento,
                p.Passageiro.DataNascimento,

                p.Voo.Id,
                p.Voo.DataVoo,
                p.Voo.Status)
            ).ToListAsync();
            return vooPassageiro;
        }
        public async Task<PassagemSalvaContrato> ComprarPassagemAsync(ComprarPassagemContrato contrato, CancellationToken ct)
        {
            var passageiro = await passageiroRep.ObterPorIdAsync(contrato.PassageiroId, ct);
            if (passageiro is null)
                throw new CustomException("{PassageiroId} Passageiro não localizado");
            else if (passageiro.Excluido)
                throw new CustomException("{PassageiroExcluido} Passageiro excluido");

            var voo = await vooRep.ObterPorIdAsync(contrato.VooId, ct);
            if (voo is null)
                throw new CustomException("{VooId} Voo não localizado");
            else if (voo.Excluido)
                throw new CustomException("{VooExcluido} Voo esta excluido");
            else if (voo.Status != VooStatus.Pendente)
                throw new CustomException("{VooStatus} Voo não esta mais disponível");
            else if (voo.QtdPassagensVendidas + 1 > voo.Aviao.QtdMaxPassageiros * 1.15)
                throw new CustomException($"{{VooQtdPassagensVendidas}} Quantidade de passageiros chegou ao limite. {voo.QtdPassagensVendidas + 1} > {voo.Aviao.QtdMaxPassageiros * 1.15}");

            var duplicado = voo.VoosPassageiros
                .Where(vp => !vp.Excluido && vp.PassageiroId == passageiro.Id)
                .FirstOrDefault();
            if (duplicado is not null)
                throw new CustomException("{PassageiroId} Já existe um ticket para esse passageiro: " + duplicado.Id);

            var vooPassageiro = vooPassageiroConv.ConverterParaEntidade(passageiro.Id, voo.Id);

            var salvo = await vooPassageiroRep.AdicionarAsync(vooPassageiro, ct);
            if (salvo.Id == Guid.Empty)
                throw new CustomException("Não foi possivel concluir o procedimento");

            return new PassagemSalvaContrato(
                    salvo.Id,
                    salvo.Status,
                    passageiro.Id,
                    passageiro.Nome,
                    passageiro.Documento,
                    passageiro.DataNascimento,
                    voo.Id,
                    voo.DataVoo,
                    voo.Status);
        }
        public async Task<bool> CheckinPassagemAsync(Guid id, CancellationToken ct)
        {
            var passagem = await vooPassageiroRep.ObterPorIdAsync(id, ct);
            if (passagem is null)
                throw new CustomException("{vooPassageiroId} passagem não encontrada");
            else if (passagem.Excluido)
                throw new CustomException("{vooPassageiroExcluido} passagem excluida");
            else if (passagem.Status != VooPassageiroStatus.Pendente)
                throw new CustomException("{vooPassageiroStatus} status não permite");

            passagem.AlterarStatus(VooPassageiroStatus.Presente);
            passagem.Voo.CheckinPassageiro();
            var salvo = await vooPassageiroRep.AtualizarAsync(passagem, ct);


            if (true) //(salvo.Voo.QtdPassageirosPresentes >= salvo.Voo.Aviao.QtdMaxPassageiros)
            {
                await mensageria.PublicarMensagemAsync(
                    new OverbookingMensagem(passagem.VooId),
                    ct);
            }
            

            return true;
        }
        public async Task<Guid> DeletarPassagemAsync(Guid id, CancellationToken ct)
        {
            var passagem = await vooPassageiroRep.ObterPorIdAsync(id, ct);
            if (passagem is null)
                throw new CustomException("{vooPassageiroId} entidade não encontrada");
            else if (passagem.Excluido)
                throw new CustomException("{vooPassageiroExcluido} entidade já esta excluída.");
            else if (passagem.Status != VooPassageiroStatus.Pendente)
                throw new CustomException("{vooPassageiroStatus} status da passagem não permite");

            passagem.Excluir();
            passagem.Voo.RetirarPassageiro();

            var salvo = await vooPassageiroRep.AtualizarAsync(passagem, ct);

            return salvo.Id;
        }
    

    }


}
