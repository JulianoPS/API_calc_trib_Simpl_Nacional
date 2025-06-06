using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APISimplesNacional.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TabelaINSSController : ControllerBase
    {
        private readonly ITabelaINSSService _service;

        public TabelaINSSController(ITabelaINSSService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtém as tabelas de INSS de uma empresa pelo e-mail ou celular.
        /// Se não encontrada, retorna a tabela da empresa padrão (ID = 1).
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TabelaINSSDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Obter(
            [FromQuery] string? email,
            [FromQuery] string? celular)
        {
            try
            {
                var result = await _service.ObterPorEmailOuCelularAsync(email, celular);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { erro = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza as tabelas de INSS de uma empresa identificada por e-mail ou celular.
        /// Não é permitido atualizar a empresa padrão (ID = 1).
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar(
            [FromQuery] string? email,
            [FromQuery] string? celular,
            [FromBody] IEnumerable<TabelaINSSDto> dto)
        {
            try
            {
                await _service.AtualizarAsync(email, celular, dto);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}