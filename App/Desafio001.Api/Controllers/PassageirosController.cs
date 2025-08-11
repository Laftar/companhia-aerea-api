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
    public class PassageirosController : ControllerBase
    {
        private readonly IMediator mediador;
        private readonly IPassageiroConversorApp conversor;
        public PassageirosController(
            IMediator mediator,
            IPassageiroConversorApp conversor)
        {
            this.mediador = mediator;
            this.conversor = conversor;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get([FromRoute] Guid id, CancellationToken ct)
        {
            var consulta = new BuscarPassageiroConsulta(id);
            var res = await mediador.Send(consulta, ct);
            return Ok(res);
        }
        [HttpGet]
        public async Task<IActionResult> Listar([FromQuery] ListarPassageirosViewModel vm, CancellationToken ct)
        {
            var contrato = conversor.ConverterContrato(vm);
            var consulta = new ListarPassageirosConsulta(contrato);
            var contratoRes = await mediador.Send(consulta, ct);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);
            return Ok(viewModelRes);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar Passageiro",
            Description = @"Validações:

Nome obrigatorio ==============================================================

{
  ""documento"": ""123.456.789-00"",
  ""dataNascimento"": ""2000-01-01"",
  ""email"": ""email@gmail.com.br""
}






Documento obrigatorio e em formato correto ===============================

{
  ""nome"": ""Passageiro Idoso"",
  ""dataNascimento"": ""2000-01-01"",
  ""email"": ""email@gmail.com.br""
}

{
  ""nome"": ""Passageiro Idoso"",
  ""documento"": ""123.456.7789-00"",
  ""dataNascimento"": ""2000-01-01"",
  ""email"": ""email@gmail.com.br""
}






Data nascimento nao pode ser no futuro ===============================

{
  ""nome"": ""Passageiro Idoso"",
  ""documento"": ""123.456.789-00"",
  ""dataNascimento"": ""2026-01-01"",
  ""email"": ""email@gmail.com.br""
}






Email precisa ser valido ===============================

{
  ""nome"": ""Passageiro Idoso"",
  ""documento"": ""123.456.789-00"",
  ""dataNascimento"": ""2000-01-01"",
  ""email"": ""emailgmail.com.br""
}

            "
        )]
        [SwaggerRequestExample(typeof(CriarPassageiroViewModel), typeof(CriarPassageiroExemplo))]
        public async Task<IActionResult> Post([FromBody] CriarPassageiroViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contrato = conversor.ConverterContrato(vm);
            var cmd = new CriarPassageiroComando(contrato);
            var contratoRes = await mediador.Send(cmd);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);

            return Ok(viewModelRes);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation(
            Summary = "Atualizar Passageiro",
            Description = @"Validações:

                ===== Entradas invalidas =====
                1 - Ao menos 1 campo precisa ser informado
                2 - Nome do passageiro com ate 150 caracteres
                3 - Data de nascimento não pode ser no futuro
                4 - Email precisa ser informado de forma válida

Ao menos um dos campos tem que ser informado ===============================

{
  ""nome"": """",
  ""email"": """"
}






Data de nascimento nao pode ser futuro ===============================

{ 
    ""nome"": ""Passageiro Idoso"",
    ""dataNascimento"": ""2026-01-01"",
    ""email"": ""email@gmail.com.br""
}






Email precisa ser informado de forma válida ===============================

{ 
    ""nome"": ""Passageiro Idoso"",
    ""dataNascimento"": ""2000-01-01"",
    ""email"": ""emailgmail.com.br""
}


            "
        )]
        [SwaggerRequestExample(typeof(AtualizarPassageiroViewModel), typeof(AtualizarPassageiroExemplo))]
        public async Task<IActionResult> Put(
            [FromRoute] Guid id, 
            [FromBody] AtualizarPassageiroViewModel vm, 
            CancellationToken ct )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contrato = conversor.ConverterContrato(vm);
            var cmd = new AtualizarPassageiroComando(id, contrato);
            var contratoRes = await mediador.Send(cmd, ct);
            var viewmodel = conversor.ConverterViewModel(contratoRes);
            return Ok(viewmodel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var cmd = new DeletarPassageiroComando(id);
            var res = await mediador.Send(cmd, ct);
            return Ok(res);
        }
    }
}