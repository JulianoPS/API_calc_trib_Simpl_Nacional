using APISimplesNacional.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace APISimplesNacional.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AtividadesController : ControllerBase
    {
        private readonly IAtividadeService _atividadeService;

        public AtividadesController(IAtividadeService atividadeService)
        {
            _atividadeService = atividadeService;
        }

        /// <summary>
        /// Retorna a lista de atividades possíveis para Simples Nacional (Anexo III ou V).
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<string>), StatusCodes.Status200OK)]
        public async Task<IActionResult> ObterTodas()
        {
            var lista = await _atividadeService.ObterTodasAsync();
            return Ok(lista);
        }
    }
}
