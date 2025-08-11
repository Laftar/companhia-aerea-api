using Desafio001.Dominio.Exceptions;
using Desafio001.Dominio.Interfaces;
using Desafio001.Dominio.Utils;
using Desafio001.Servico.Mensageria;
using RabbitMQ.Client;
using System.Reflection;
using System.Text;
using System.Text.Json;

namespace Desafio001.Servico.Servicos
{
    public class MensageriaProdutorServico : IMensageriaProdutorServico
    {

        private readonly IMensageriaConexao conexao;
        public MensageriaProdutorServico(IMensageriaConexao conexao)
        {
            this.conexao = conexao;
        }

        public async Task PublicarMensagemAsync<T>(T mensagem, CancellationToken ct)
        {

            var fila = typeof(T).GetCustomAttribute<Fila>();
            if (fila == null)
                throw new CustomException($"Classe {typeof(T).Name} não possui o atributo [Fila].");

            var canal = await conexao
                .BuscarCanalAsync(fila.Nome, ct)
                .ConfigureAwait(false);

            var json = JsonSerializer.Serialize(mensagem);
            var body = Encoding.UTF8.GetBytes(json);

            await canal.BasicPublishAsync(
                    exchange: string.Empty,
                    routingKey: fila.Nome,
                    body: body,
                    cancellationToken: ct)
                .ConfigureAwait(false);
        }
    }
}
