using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APISimplesNacional.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CalculosController : ControllerBase
    {
        private readonly ICalculoInssService _inssService;
        private readonly ICalculoIrService _irService;
        private readonly ICalculoDespesaService _calculoService;
        private readonly IAtividadeService _atividadeService;

        public CalculosController(
            ICalculoInssService inssService,
            ICalculoIrService irService,
            ICalculoDespesaService calculoService,
            IAtividadeService atividadeService)
        {
            _inssService = inssService;
            _irService = irService;
            _calculoService = calculoService;
            _atividadeService = atividadeService;
        }

        /// <summary>
        /// Calcula o INSS de uma lista de sócios.
        /// </summary>
        [HttpPost("inss/socios")]
        [ProducesResponseType(typeof(IEnumerable<SocioResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalcularInssSocios(
            [FromQuery] string? email,
            [FromQuery] string? celular,
            [FromBody] IEnumerable<SocioDto> socios)
        {
            var result = await _inssService.CalcularInssSociosAsync(socios, email, celular);
            return Ok(result);
        }

        /// <summary>
        /// Calcula o INSS de uma lista de funcionários.
        /// </summary>
        [HttpPost("inss/funcionarios")]
        [ProducesResponseType(typeof(IEnumerable<FuncionarioResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalcularInssFuncionarios(
            [FromQuery] string? email,
            [FromQuery] string? celular,
            [FromBody] IEnumerable<FuncionarioDto> funcionarios)
        {
            var result = await _inssService.CalcularInssFuncionariosAsync(funcionarios, email, celular);
            return Ok(result);
        }

        /// <summary>
        /// Calcula o IR de uma lista de sócios.
        /// </summary>
        [HttpPost("ir/socios")]
        [ProducesResponseType(typeof(IEnumerable<SocioResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalcularIrSocios(
            [FromQuery] string? email,
            [FromQuery] string? celular,
            [FromBody] IEnumerable<SocioDto> socios)
        {
            var result = await _irService.CalcularIrSociosAsync(socios, email, celular);
            return Ok(result);
        }

        /// <summary>
        /// Calcula o IR de uma lista de funcionários.
        /// </summary>
        [HttpPost("ir/funcionarios")]
        [ProducesResponseType(typeof(IEnumerable<FuncionarioResponseDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalcularIrFuncionarios(
            [FromQuery] string? email,
            [FromQuery] string? celular,
            [FromBody] IEnumerable<FuncionarioDto> funcionarios)
        {
            var result = await _irService.CalcularIrFuncionariosAsync(funcionarios, email, celular);
            return Ok(result);
        }

        /// <summary>
        /// Calcula a despesa completa e tributos do Simples Nacional:
        /// - Faturamento mensal e anual
        /// - Despesas fixas mensais e anuais
        /// - Cálculo de INSS e IR para sócios e funcionários
        /// - Folha de salários (bruta + encargos patronais: CPP 20% e FGTS 8%)
        /// - Fator R (Folha / Faturamento)
        /// - Seleção de Anexo (III ou V)
        /// - Alíquota nominal, efetiva e valor do DAS a pagar
        /// </summary>
        [HttpPost("simples-nacional")]
        [ProducesResponseType(typeof(CalculoResponseDto), StatusCodes.Status200OK)]
        public async Task<IActionResult> CalcularSimplesNacional([FromBody] CalculoRequestDto request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            bool valida = await _atividadeService.AtividadeValidaAsync(request.Atividade);
            if (!valida)
            {
                return BadRequest(new { mensagem = "A atividade selecionada não está válida para Simples Nacional." });
            }

            if (request.Atividade.Equals("Minha atividade não está na lista", System.StringComparison.OrdinalIgnoreCase))
            {
                return BadRequest(new { mensagem = "A atividade selecionada não está sujeita ao Fator R!" });
            }

            var result = await _calculoService.CalcularAsync(request);
            return Ok(result);
        }
    }
}
