using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Application.Dtos;
using Microsoft.AspNetCore.Mvc;
using APISimplesNacional.Domain.Interfaces;
using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EmpresasController : ControllerBase
    {
        private readonly IEmpresaService _empresaService;
        private readonly ICalculoDespesaService _calculoService;

        public EmpresasController(IEmpresaService empresaService, ICalculoDespesaService calculoService)
        {
            _empresaService = empresaService;
            _calculoService = calculoService;
        }

        [HttpGet("testar-erro")]
        public IActionResult TestarErro()
        {
            throw new Exception("Erro de teste");
        }

        /// <summary>
        /// Cadastra uma nova empresa.
        /// Após o cadastro, será possível atualizar as tabelas do Simples Nacional.
        /// As próximas interações poderão ser feitas com base no celular ou email informado.
        /// </summary>
        /// <param name="dto">Dados obrigatórios: Nome, Celular, Email.</param>
        /// <returns>Empresa cadastrada com sucesso</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Post([FromBody] CriarEmpresaDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var criada = await _empresaService.CriarEmpresaComTabelasAsync(dto);
            return CreatedAtAction(nameof(Post), new { id = criada.Id }, criada);
        }


        /// <summary>
        /// Busca a empresa pelo e‑mail ou celular.
        /// Se não encontrar, retorna 404.
        /// </summary>
        [HttpGet("por-contato")]
        [ProducesResponseType(typeof(Empresas), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPorContato(
            [FromQuery] string? email,
            [FromQuery] string? celular)
        {
            var emp = await _empresaService.ObterPorEmailOuCelularAsync(email, celular );
            if (emp == null)
                return NotFound(new { mensagem = "Empresa não encontrada." });

            var response = new EmpresaResponseDto
            {
                Nome = emp.Nome,
                Celular = emp.Celular,
                Email = emp.Email,
                IrDependente = emp.IrDependente
            };

            return Ok(response);
        }

        /// <summary>
        /// Atualiza o valor de desconto por dependente (irDependente) da empresa.
        /// Empresa padrão (ID 1) não pode ser alterada.
        /// </summary>
        [HttpPut("ir-dependente")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> AtualizarIrDependente(
            [FromQuery] string? email,
            [FromQuery] string? celular,
            [FromBody] UpdateIrDependenteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                await _empresaService.AtualizarIrDependenteAsync(
                    email, celular, dto.IrDependente);
                return NoContent();
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { mensagem = ex.Message });
            }
        }
    }
}

