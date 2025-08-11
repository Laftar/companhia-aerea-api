using Desafio001.Api.Conversores;
using Desafio001.Api.ViewModels;
using Desafio001.Api.ViewModels.Exemplos;
using Desafio001.Mediador.Comandos;
using Desafio001.Mediador.Consultas;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.Filters;

namespace Desafio001.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AvioesController : ControllerBase
    {
        private readonly IMediator mediador;
        private readonly IAviaoConversorApp conversor;
        public AvioesController(
            IMediator mediator,
            IAviaoConversorApp conversor)
        {
            mediador = mediator;
            this.conversor = conversor;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken ct)
        {
            var consulta = new BuscarAviaoConsulta(id);
            var contratoRes = await mediador.Send(consulta, ct);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);
            return Ok(viewModelRes);
        }

        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ListarAvioesViewModel vm, CancellationToken ct)
        {
            var contrato = conversor.ConverterContrato(vm);
            var consulta = new ListarAvioesConsulta(contrato);
            var contratoRes = await mediador.Send(consulta, ct);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);
            return Ok(viewModelRes);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar Aviao",
            Description = @"Validações:

Marca não pode ser vazia ===============================

{
    ""marca"": """",
    ""modelo"": ""737-800"",
    ""anoFabricacao"": 2015,
    ""qtdMaxPassageiros"": 100,
    ""qtdVoosRealizados"": 0
}







Modelo não pode ser vazio ===============================

{
    ""marca"": ""Boeing"",
    ""modelo"": """",
    ""anoFabricacao"": 2015,
    ""qtdMaxPassageiros"": 100,
    ""qtdVoosRealizados"": 0
}







Ano de fabricação não pode ser no futuro ===============================

{
    ""marca"": ""Boeing"",
    ""modelo"": ""737-800"",
    ""anoFabricacao"": 2026,
    ""qtdMaxPassageiros"": 100,
    ""qtdVoosRealizados"": 0
}







quantidade maxima de passageiros e voos realizados nao pode ser negativo ===============================

{
    ""marca"": ""Boeing"",
    ""modelo"": ""737-800"",
    ""anoFabricacao"": 2026,
    ""qtdMaxPassageiros"": -1,
    ""qtdVoosRealizados"": -1
}

            "
        )]
        [SwaggerRequestExample(typeof(CriarAviaoViewModel), typeof(CriarAviaoExemplo))]
        public async Task<IActionResult> Post([FromBody] CriarAviaoViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contrato = conversor.ConverterContrato(vm);
            var cmd = new CriarAviaoComando(contrato);
            var contratoRes = await mediador.Send(cmd);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);

            return Ok(viewModelRes);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation(
            Summary = "Atualizar Aviao",
            Description = @"Validações:

Ao menos um dos campos tem que ser informado ===============================

{
  ""modelo"": """"
}






Ano de fabricação não pode ser no futuro ===============================

{
    ""anoFabricacao"": 2026
}

            "
        )]
        [SwaggerRequestExample(typeof(AtualizarAviaoViewModel), typeof(AtualizarAviaoExemplo))]
        public async Task<IActionResult> Put(
            [FromRoute] Guid id, 
            [FromBody] AtualizarAviaoViewModel vm, 
            CancellationToken ct )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contrato = conversor.ConverterContrato(vm);
            var cmd = new AtualizarAviaoComando(id, contrato);
            var contratoRes = await mediador.Send(cmd, ct);
            var viewmodel = conversor.ConverterViewModel(contratoRes);
            return Ok(viewmodel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var cmd = new DeletarAviaoComando(id);
            var res = await mediador.Send(cmd, ct);
            return Ok(res);
        }
    }
}