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
    public class AviaoServico(
        IRepositorioBase<Aviao> repositorio, 
        IAviaoConversor conversor,
        ILogger<AviaoServico> logger,
        IMensageriaProdutorServico mensageria) : IAviaoServico
    {
        private readonly IRepositorioBase<Aviao> repositorio = repositorio;
        private readonly IAviaoConversor conversor = conversor;
        private readonly ILogger<AviaoServico> logger = logger;
        private readonly IMensageriaProdutorServico mensageria = mensageria;

        public async Task<AviaoSalvoContrato> BuscarAviaoAsync(Guid id, CancellationToken ct)
        {
            var aviao = await repositorio.ObterPorIdAsync(id, ct);
            if (aviao is null)
                throw new CustomException("{AviaoId} entidade não encontrada");
            else if (aviao.Excluido)
                throw new CustomException("{AviaoExcluido} Entidade esta deletada");

            return new AviaoSalvoContrato(
                aviao.Id,
                aviao.Marca,
                aviao.Modelo,
                aviao.AnoFabricacao,
                aviao.QtdMaxPassageiros,
                aviao.QtdVoosRealizados
            );
        }
        public async Task<List<AviaoSalvoContrato>> ListarAvioesAsync(ListarAvioesContrato contrato, CancellationToken ct)
        {

            var query = repositorio.MontarConsulta();

            if (!string.IsNullOrWhiteSpace(contrato.Marca))
                query = query.Where(a => a.Marca.Contains(contrato.Marca));

            if (!string.IsNullOrWhiteSpace(contrato.Modelo))
                query = query.Where(a => a.Modelo.Contains(contrato.Modelo));

            var avioes = query.Select(a => new AviaoSalvoContrato(
                a.Id,
                a.Marca,
                a.Modelo,
                a.AnoFabricacao,
                a.QtdMaxPassageiros,
                a.QtdVoosRealizados));

            return await avioes.ToListAsync(ct);
        }
        public async Task<AviaoSalvoContrato> CriarAviaoAsync(CriarAviaoContrato contrato, CancellationToken ct)
        {
            var aviao = conversor.ConverterParaEntidade(contrato);

            var salvo = await repositorio.AdicionarAsync(aviao, ct);

            if (salvo.Id == Guid.Empty)
                throw new CustomException("Não foi possivel concluir o procedimento");

            return new AviaoSalvoContrato(
                Id : salvo.Id,
                Marca : salvo.Marca,
                Modelo : salvo.Modelo,
                AnoFabricacao: salvo.AnoFabricacao,
                QtdMaxPassageiros : salvo.QtdMaxPassageiros,
                QtdVoosRealizados :salvo.QtdVoosRealizados
            );

        }
        public async Task<AviaoSalvoContrato> AtualizarAviaoAsync(Guid id, AtualizarAviaoContrato contrato, CancellationToken ct)
        {
            var aviao = await repositorio.ObterPorIdAsync(id, ct);
            if (aviao is null)
                throw new CustomException("{AviaoId} entidade não encontrada");
            else if (aviao.Excluido)
                throw new CustomException("{AviaoExcluido} Entidade esta deletada");

            aviao.AtualizarDados(
                contrato.Modelo,
                contrato.AnoFabricacao
                );

            var salvo = await repositorio.AtualizarAsync(aviao, ct);

            return new AviaoSalvoContrato(
                Id: salvo.Id,
                Marca: salvo.Marca,
                Modelo: salvo.Modelo,
                AnoFabricacao: salvo.AnoFabricacao,
                QtdMaxPassageiros: salvo.QtdMaxPassageiros,
                QtdVoosRealizados: salvo.QtdVoosRealizados
            );

        }
        public async Task<Guid> DeletarAviaoAsync(Guid id, CancellationToken ct)
        {
            var aviao = await repositorio.ObterPorIdAsync(id, ct);
            if (aviao is null)
                throw new CustomException("{AviaoId} Entidade não encontrada");
            else if (aviao.Excluido)
                throw new CustomException("{AviaoExcluido} Entidade já esta deletada");

            aviao.Excluir();
            foreach (var voo in aviao.Voos)
            {
                voo.Excluir();
                await mensageria.PublicarMensagemAsync(
                    new CancelamentoVooMensagem(voo.Id),
                    ct);
            }

            var salvo = await repositorio.AtualizarAsync(aviao, ct);

            return salvo.Id;

        }


    }
}
