using Desafio001.Dominio.Contratos;
using Desafio001.Dominio.Entidades;
using Desafio001.Dominio.Exceptions;
using Desafio001.Dominio.Interfaces;
using Desafio001.Servico.Conversores;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Desafio001.Servico.Servicos
{
    public class PassageiroServico(
        IRepositorioBase<Passageiro> repositorio,
        IPassageiroConversor conversor,
        ILogger<PassageiroServico> logger) : IPassageiroServico
    {
        private readonly IRepositorioBase<Passageiro> repositorio = repositorio;
        private readonly IPassageiroConversor conversor = conversor;
        private readonly ILogger<PassageiroServico> logger = logger;


        public async Task<PassageiroSalvoContrato> BuscarPassageiroAsync(Guid id, CancellationToken ct)
        {
            var passageiro = await repositorio.ObterPorIdAsync(id, ct);

            if (passageiro is null)
                throw new CustomException("Entidade passageiro não localizado");
            else if (passageiro.Excluido)
                throw new CustomException("Entidade passageiro já excluida");

            return new PassageiroSalvoContrato(
                passageiro.Id,
                passageiro.Nome,
                passageiro.Documento,
                passageiro.DataNascimento,
                passageiro.Email
            );
        }
        public async Task<List<PassageiroSalvoContrato>> ListarPassageirosAsync(ListarPassageirosContrato contrato, CancellationToken ct)
        {
            var query = repositorio
                .MontarConsulta()
                .Where(p => !p.Excluido);

            if (!string.IsNullOrWhiteSpace(contrato.Nome))
                query = query.Where(p => p.Nome.Contains(contrato.Nome));

            if (!string.IsNullOrWhiteSpace(contrato.Documento))
                query = query.Where(p => p.Documento.Contains(contrato.Documento));

            var passageiros = await query.Select(p => new PassageiroSalvoContrato(
                    p.Id,
                    p.Nome,
                    p.Documento,
                    p.DataNascimento,
                    p.Email)).ToListAsync(ct);

            return passageiros;
        }
        public async Task<PassageiroSalvoContrato> CriarPassageiroAsync(CriarPassageiroContrato contrato, CancellationToken ct)
        {

            //if (!DocumentoUtils.CpfValido(contrato.Documento))
            //    throw new CustomException("CPF inválido", 404);

            var passageiro = conversor.ConverterParaEntidade(contrato);

            var duplicados = await ListarPassageirosAsync(new ListarPassageirosContrato(
                Nome: null,
                Documento: passageiro.Documento), ct);
            if (duplicados.Count > 0)
                throw new CustomException("CPF Duplicado");

            var salvo = await repositorio.AdicionarAsync(passageiro, ct);
            if (salvo.Id == Guid.Empty)
                throw new CustomException("Não foi possivel concluir o procedimento");

            return new PassageiroSalvoContrato(
                Id: salvo.Id,
                Nome: salvo.Nome,
                Documento: salvo.Documento,
                DataNascimento: salvo.DataNascimento,
                Email: salvo.Email
            );

        }
        public async Task<PassageiroSalvoContrato> AtualizarPassageiroAsync(Guid id, AtualizarPassageiroContrato contrato, CancellationToken ct)
        {
            var passageiro = await repositorio.ObterPorIdAsync(id, ct);
            if (passageiro is null)
                throw new CustomException("{PassageiroId} entidade não encontrada");
            else if (passageiro.Excluido)
                throw new CustomException("{PassageiroId} entidade excluida");

            passageiro.AtualizarDados(
                contrato.Nome,
                contrato.DataNascimento,
                contrato.Email
                );

            var salvo = await repositorio.AtualizarAsync(passageiro, ct);

            return new PassageiroSalvoContrato(
                Id: salvo.Id,
                Nome: salvo.Nome,
                Documento: salvo.Documento,
                DataNascimento: salvo.DataNascimento,
                Email: salvo.Email
            );

        }
        public async Task<Guid> DeletarPassageiroAsync(Guid id, CancellationToken ct)
        {
            var passageiro = await repositorio.ObterPorIdAsync(id, ct);
            if (passageiro is null)
                throw new CustomException("{PassageiroId} entidade não encontrada");
            else if (passageiro.Excluido)
                throw new CustomException("{PassageiroExcluido} entidade já esta excluida");

            passageiro.Excluir();
            foreach (var passagem in passageiro.VoosPassageiros.Where(p => !p.Excluido))
            {
                passagem.Excluir();
                passagem.Voo.RetirarPassageiro();
            }
            
            var salvo = await repositorio.AtualizarAsync(passageiro, ct);

            return id;

        }

    }
}
