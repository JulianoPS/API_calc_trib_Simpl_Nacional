using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace APISimplesNacional.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class TabelaIRController : ControllerBase
    {
        private readonly ITabelaIRService _service;
        public TabelaIRController(ITabelaIRService service)
        {
            _service = service;
        }

        /// <summary>
        /// Obtém as tabelas IR de uma empresa pelo e-mail ou celular. 
        /// Se não encontrado, retorna dados da empresa padrão (ID = 1).
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TabelaIRDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Obter([FromQuery] string? email, [FromQuery] string? celular)
        {
            try
            {
                var result = await _service.ObterPorEmailOuCelularAsync(email, celular);
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(new { Erro = ex.Message });
            }
        }

        /// <summary>
        /// Atualiza as tabelas IR de uma empresa identificada por e-mail ou celular.
        /// Não é permitido atualizar os dados da empresa padrão (ID = 1).
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Atualizar([FromQuery] string? email, [FromQuery] string? celular, [FromBody] IEnumerable<TabelaIRDto> dto)
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
