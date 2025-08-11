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
    public class VoosController : ControllerBase
    {
        private readonly IMediator mediador;
        private readonly IVooConversorApp conversor;
        public VoosController(
            IMediator mediator,
            IVooConversorApp conversor)
        {
            mediador = mediator;
            this.conversor = conversor;
        }

        [HttpGet]
        public async Task<IActionResult> ListarVoos([FromQuery] ListarVoosViewModel vm, CancellationToken ct)
        {
            var contrato = conversor.ConverterContrato(vm);
            var consulta = new ListarVoosConsulta(contrato);
            var contratoRes = await mediador.Send(consulta, ct);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);
            return Ok(viewModelRes);
        }

        [HttpPost]
        [SwaggerOperation(
            Summary = "Criar Voo",
            Description = @"Validações:


Todos os campos obrigatorios ==================================
{
  ""aviaoId"": """",
  ""pilotoId"": ""1c21edd7-0e6b-f011-8bf7-e4a8dff3558a"",
  ""dataVoo"": ""2025-08-10"",
  ""horarioPrevSaida"": ""08:00:00"",
  ""horarioPrevChegada"": ""10:00:00""
}






Previsao só pode ser no futuro ===============================

{
  ""aviaoId"": ""e8b49301-0e6b-f011-8bf7-e4a8dff3558a"",
  ""pilotoId"": ""1c21edd7-0e6b-f011-8bf7-e4a8dff3558a"",
  ""dataVoo"": ""2023-08-10"",
  ""horarioPrevSaida"": ""08:00:00"",
  ""horarioPrevChegada"": ""10:00:00""
}

{
  ""aviaoId"": ""e8b49301-0e6b-f011-8bf7-e4a8dff3558a"",
  ""pilotoId"": ""1c21edd7-0e6b-f011-8bf7-e4a8dff3558a"",
  ""dataVoo"": ""2025-08-08"",
  ""horarioPrevSaida"": ""08:00:00"",
  ""horarioPrevChegada"": ""10:00:00""
}

            "
        )]
        [SwaggerRequestExample(typeof(CriarVooViewModel), typeof(CriarVooExemplo))]
        public async Task<IActionResult> CriarVoo([FromBody] CriarVooViewModel vm, CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contrato = conversor.ConverterContrato(vm);
            var cmd = new CriarVooComando(contrato);
            var contratoRes = await mediador.Send(cmd, ct);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);

            return Ok(viewModelRes);
        }

        [HttpPut("{id:guid}")]
        [SwaggerOperation(
            Summary = "Atualizar Voo",
            Description = @"Validações:

Só é possivel atualizar pro futuro previsao ===============================

{
  ""dataVoo"": ""2023-08-08"",
  ""horarioPrevSaida"": ""10:00:00"",
  ""horarioPrevChegada"": ""12:00:00""
}
{
  ""dataVoo"": ""2025-08-08"",
  ""horarioPrevSaida"": ""10:00:00"",
  ""horarioPrevChegada"": ""12:00:00""
}

            "
        )]
        [SwaggerRequestExample(typeof(AtualizarVooViewModel), typeof(AtualizarVooExemplo))]
        public async Task<IActionResult> AtualizarVoo([FromRoute] Guid id, AtualizarVooViewModel vm, CancellationToken ct)
        {
            var contrato = conversor.ConverterContrato(vm);
            var cmd = new AtualizarVooComando(id, contrato);
            var contratoRes = await mediador.Send(cmd, ct);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);

            return Ok(viewModelRes);
        }
        [HttpPut("{id:guid}/concluir")]
        [SwaggerOperation(
            Summary = "Concluir Voo",
            Description = @"Validações:

Horario real de saida e chegada precisam ser validos ===============================

{
  ""horarioRealSaida"": ""09:00:00""
}

{
  ""horarioRealSaida"": ""11:00:00"",
  ""horarioRealChegada"": ""09:00:00""
}

            "
        )]
        [SwaggerRequestExample(typeof(ConcluirVooViewModel), typeof(ConcluirVooExemplo))]
        public async Task<IActionResult> ConcluirVoo([FromRoute] Guid id, ConcluirVooViewModel vm, CancellationToken ct)
        {
            var contrato = conversor.ConverterContrato(vm);
            var cmd = new ConcluirVooComando(id, contrato);
            var contratoRes = await mediador.Send(cmd, ct);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);

            return Ok(viewModelRes);
        }
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeletarVoo([FromRoute] Guid id, CancellationToken ct)
        {
            var cmd = new DeletarVooComando(id);
            var res = await mediador.Send(cmd, ct);
            return Ok(res);
        }

        [HttpGet("passagens")]
        public async Task<IActionResult> ListarPassagens([FromQuery] ListarPassagensViewModel vm, CancellationToken ct)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contrato = conversor.ConverterContrato(vm);
            var consulta = new ListarPassagensConsulta(contrato);
            var contratoRes = await mediador.Send(consulta, ct);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);
            return Ok(viewModelRes);

        }
        [HttpPost("passagens")]
        [SwaggerOperation(
            Summary = "Comprar Passagem",
            Description = @"Validações:

Precisa ser informado ambos os Id ===============================

{
  ""passageiroId"": ""a05cacbc-4572-f011-8bf9-e4a8dff3558a"",
  ""vooId"": """"
}


            "
        )]
        [SwaggerRequestExample(typeof(ComprarPassagemViewModel), typeof(ComprarPassagemExemplo))]
        public async Task<IActionResult> ComprarPassagem([FromBody] ComprarPassagemViewModel vm)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            var contrato = conversor.ConverterContrato(vm);
            var cmd = new ComprarPassagemComando(contrato);
            var contratoRes = await mediador.Send(cmd);
            var viewModelRes = conversor.ConverterViewModel(contratoRes);

            return Ok(viewModelRes);
        }
        [HttpPut("passagens/{id:guid}/check-in")]
        public async Task<IActionResult> CheckinPassagem([FromRoute] Guid id, CancellationToken ct)
        {
            var cmd = new CheckinPassagemComando(id);
            var ok = await mediador.Send(cmd, ct);
            return Ok(ok);
        }
        [HttpDelete("passagens/{id:guid}")]
        public async Task<IActionResult> DeletarPassagem([FromRoute] Guid id, CancellationToken ct)
        {
            var cmd = new DeletarPassagemComando(id);
            var res = await mediador.Send(cmd, ct);
            return Ok(res);
        }

    }
}