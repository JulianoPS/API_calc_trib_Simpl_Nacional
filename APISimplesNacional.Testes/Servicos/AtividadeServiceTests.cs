using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Application.Services;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace APISimplesNacional.Testes.Services
{
    public class AtividadeServiceTests
    {
        private readonly IAtividadeService _service = new AtividadeService();

        [Fact]
        public async Task ObterTodasAsync_TemListaNaoVazia()
        {
            var lista = await _service.ObterTodasAsync();
            Assert.NotEmpty(lista);
            Assert.Contains("Fisioterapia", lista);
            Assert.Contains("Minha atividade não está na lista", lista);
        }

        [Theory]
        [InlineData("Fisioterapia", true)]
        [InlineData("fisioterapia", true)] // ignora caixa
        [InlineData("Medicina veterinária", true)]
        [InlineData("Atividade Inexistente", false)]
        [InlineData("", false)]
        [InlineData(null, false)]
        public async Task AtividadeValidaAsync_RetornaCorreto(string atividade, bool esperado)
        {
            var resultado = await _service.AtividadeValidaAsync(atividade);
            Assert.Equal(esperado, resultado);
        }
    }
}
