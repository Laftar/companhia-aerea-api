using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Entidades;
using Desafio001.Dominio.Exceptions;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Mensagens;
using Desafio001.Servico.Conversores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;


namespace Desafio001.Servico.Servicos
{
    public class PilotoServico(
        IRepositorioBase<Piloto> repositorio, 
        IPilotoConversor conversor,
        ILogger<PilotoServico> logger,
        IMensageriaProdutorServico mensageria) : IPilotoServico
    {
        private readonly IRepositorioBase<Piloto> repositorio = repositorio;
        private readonly IPilotoConversor conversor = conversor;
        private readonly ILogger<PilotoServico> logger = logger;
        private readonly IMensageriaProdutorServico mensageria = mensageria;

        public async Task<PilotoSalvoContrato> BuscarPilotoAsync(Guid id, CancellationToken ct)
        {
            var piloto = await repositorio.ObterPorIdAsync(id, ct);
            if (piloto is null)
                throw new CustomException("{PilotoId} Entidade não encontrada");
            else if (piloto.Excluido)
                throw new CustomException("{PilotoExcluido} Entidade esta deletada");


            return new PilotoSalvoContrato(
                piloto.Id,
                piloto.Nome,
                piloto.Documento,
                piloto.DataNascimento,
                piloto.QtdVoosRealizados
            );
        }
        public async Task<List<PilotoSalvoContrato>> ListarPilotosAsync(ListarPilotosContrato contrato, CancellationToken ct)
        {
            var query = repositorio
                .MontarConsulta()
                .Where(p => !p.Excluido);

            if (!string.IsNullOrWhiteSpace(contrato.Nome))
                query = query.Where(p => p.Nome.Contains(contrato.Nome));

            if (!string.IsNullOrWhiteSpace(contrato.Documento))
                query = query.Where(p => p.Documento.Contains(contrato.Documento));

            var pilotos = query.Select(p => new PilotoSalvoContrato(
                    p.Id,
                    p.Nome,
                    p.Documento,
                    p.DataNascimento,
                    p.QtdVoosRealizados));

            return await pilotos.ToListAsync(ct);
        }
        public async Task<PilotoSalvoContrato> CriarPilotoAsync(CriarPilotoContrato contrato, CancellationToken ct)
        {
            //if (!DocumentoUtils.CpfValido(contrato.Documento))
            //    throw new CustomException("CPF inválido", 404);

            var piloto = conversor.ConverterParaEntidade(contrato);

            var duplicados = await ListarPilotosAsync(new ListarPilotosContrato(
                Nome: null,
                Documento: piloto.Documento), ct);

            if (duplicados.Count > 0 )
                throw new CustomException("CPF Duplicado");

            var salvo = await repositorio.AdicionarAsync(piloto, ct);
            if (salvo.Id == Guid.Empty)
                throw new CustomException("Não foi possivel concluir o procedimento");

            return new PilotoSalvoContrato(
                Id : salvo.Id,
                Nome: salvo.Nome,
                Documento : salvo.Documento,
                DataNascimento: salvo.DataNascimento,
                QtdVoosRealizados: salvo.QtdVoosRealizados
            );

        }
        public async Task<PilotoSalvoContrato> AtualizarPilotoAsync(Guid id, AtualizarPilotoContrato contrato, CancellationToken ct)
        {
            var piloto = await repositorio.ObterPorIdAsync(id, ct);
            if (piloto is null)
                throw new CustomException("{PilotoId} Entidade não encontrada");
            else if (piloto.Excluido)
                throw new CustomException("{PilotoExcluido} Entidade esta deletada");

            piloto.AtualizarDados(
                contrato.Nome,
                contrato.DataNascimento,
                contrato.QtdVoosRealizados
                );

            var salvo = await repositorio.AtualizarAsync(piloto, ct);

            return new PilotoSalvoContrato(
                Id: salvo.Id,
                Nome: salvo.Nome,
                Documento: salvo.Documento,
                DataNascimento: salvo.DataNascimento,
                QtdVoosRealizados: salvo.QtdVoosRealizados
            );

        }
        public async Task<Guid> DeletarPilotoAsync(Guid id, CancellationToken ct)
        {
            var piloto = await repositorio.ObterPorIdAsync(id, ct);
            if (piloto is null)
                throw new CustomException("{PilotoId} Entidade não encontrada");
            else if (piloto.Excluido)
                throw new CustomException("{PilotoExcluido} Entidade já esta deletada");

            piloto.Excluir();
            foreach (var voo in piloto.Voos)
            {
                voo.Excluir();
                await mensageria.PublicarMensagemAsync(
                    new CancelamentoVooMensagem(voo.Id),
                    ct);
            }
                

            var salvo = await repositorio.AtualizarAsync(piloto, ct);

            return salvo.Id;
        }

    }
}
