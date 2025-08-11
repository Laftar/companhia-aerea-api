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
    public class PilotosController : ControllerBase
    {
        private readonly IMediator mediador;
        private readonly IPilotoConversorApp conversor;
        public PilotosController(
            IMediator mediator,
            IPilotoConversorApp conversor)
        {
            this.mediador = mediator;
            this.conversor = conversor;
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> RetornarPiloto([FromRoute] Guid id, CancellationToken ct)
        {
            var consulta = new BuscarPilotoConsulta(id);
            var res = await mediador.Send(consulta, ct);
            return Ok(res);
        }


        [HttpGet]
        public async Task<IActionResult> RetornarListaPiloto([FromQuery] ListarPilotosViewModel vm, CancellationToken ct)
        {
            var contrato = conversor.ConverterContrato(vm);
            var consulta = new ListarPilotosConsulta(contrato);
            var contratoRes = await mediador.Send(consulta, ct);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);
            return Ok(viewModelRes);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar Piloto",
            Description = @"Validações:
Documento de CPF valido ===============================

{
  ""nome"": ""Piloto"",
  ""documento"": ""1233.456.789-00"",
  ""dataNascimento"": ""2000-01-01"",
  ""qtdVoosRealizados"": 0
}






Piloto precisa ser de maior ===============================

{
  ""nome"": ""Piloto"",
  ""documento"": ""123.456.789-00"",
  ""dataNascimento"": ""2010-01-01"",
  ""qtdVoosRealizados"": 0
}






Quantidade de voos não pode ser negativo ===============================

{
  ""nome"": ""Piloto"",
  ""documento"": ""123.456.789-00"",
  ""dataNascimento"": ""2000-01-01"",
  ""qtdVoosRealizados"": -1
}

            "
        )]
        [SwaggerRequestExample(typeof(CriarPilotoViewModel), typeof(CriarPilotoExemplo))]
        public async Task<IActionResult> Post([FromBody] CriarPilotoViewModel vm, CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);       

            var contrato = conversor.ConverterContrato(vm);
            var cmd = new CriarPilotoComando(contrato);
            var contratoRes = await mediador.Send(cmd, ct);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);

            return Ok(viewModelRes);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation(
            Summary = "Atualizar Piloto",
            Description = @"Validações:
 Ao menos um dos campos tem que ser informado ===============================

{
  ""nome"": """"
}






Precisa ser de maior ===============================

{
  ""nome"": ""Piloto"",
  ""dataNascimento"": ""2015-01-01"",
  ""qtdVoosRealizados"": 0
}






Quantidade de voos não pode ser negativa ===============================

{
  ""nome"": ""Piloto"",
  ""dataNascimento"": ""2000-01-01"",
  ""qtdVoosRealizados"": -1
}

            "
        )]
        [SwaggerRequestExample(typeof(AtualizarPilotoViewModel), typeof(AtualizarPilotoExemplo))]
        public async Task<IActionResult> Put(
            [FromRoute] Guid id, 
            [FromBody] AtualizarPilotoViewModel vm, 
            CancellationToken ct )
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contrato = conversor.ConverterContrato(vm);
            var cmd = new AtualizarPilotoComando(id, contrato);
            var contratoRes = await mediador.Send(cmd, ct);
            var viewmodel = conversor.ConverterViewModel(contratoRes);
            return Ok(viewmodel);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete([FromRoute] Guid id, CancellationToken ct)
        {
            var cmd = new DeletarPilotoComando(id);
            var res = await mediador.Send(cmd, ct);
            return Ok(res);
        }
    }
}