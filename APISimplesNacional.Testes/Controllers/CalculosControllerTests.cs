using APISimplesNacional.API.Controllers;
using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Domain.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace APISimplesNacional.Testes.Controllers
{
    public class CalculosControllerTests
    {
        private readonly Mock<ICalculoInssService> _inssMock = new();
        private readonly Mock<ICalculoIrService> _irMock = new();
        private readonly Mock<ICalculoDespesaService> _despesaMock = new();
        private readonly Mock<IAtividadeService> _atividadeMock = new();

        private CalculosController CriarController()
        {
            return new CalculosController(
                _inssMock.Object,
                _irMock.Object,
                _despesaMock.Object,
                _atividadeMock.Object
            );
        }

        [Fact]
        public async Task CalcularSimplesNacional_AtividadeInvalida_RetornaBadRequest()
        {
            // Arrange
            var ctrl = CriarController();
            var dto = new CalculoRequestDto
            {
                Atividade = "Atividade Fora da Lista",
                FaturamentoMensal = 10000m
            };
            ctrl.ModelState.Clear();

            _atividadeMock
                .Setup(s => s.AtividadeValidaAsync(dto.Atividade))
                .ReturnsAsync(false);

            // Act
            var result = await ctrl.CalcularSimplesNacional(dto);

            // Assert
            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, bad.StatusCode);
            Assert.Contains("não está válida para Simples Nacional", bad.Value.ToString());
        }

        [Fact]
        public async Task CalcularSimplesNacional_AtividadeMinhaNaoEstaLista_RetornaBadRequestFatorR()
        {
            // Arrange
            var ctrl = CriarController();
            var dto = new CalculoRequestDto
            {
                Atividade = "Minha atividade não está na lista",
                FaturamentoMensal = 10000m
            };
            ctrl.ModelState.Clear();

            _atividadeMock
                .Setup(s => s.AtividadeValidaAsync(It.IsAny<string>()))
                .ReturnsAsync(true);

            // Act
            var result = await ctrl.CalcularSimplesNacional(dto);

            // Assert
            var bad = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, bad.StatusCode);
            Assert.Contains("não está sujeita ao Fator R", bad.Value.ToString());
        }

        [Fact]
        public async Task CalcularSimplesNacional_AtividadeValida_ChamaCalculoService()
        {
            // Arrange
            var ctrl = CriarController();
            var dto = new CalculoRequestDto
            {
                Atividade = "Fisioterapia",
                FaturamentoMensal = 12000m
            };
            ctrl.ModelState.Clear();

            _atividadeMock
                .Setup(s => s.AtividadeValidaAsync(dto.Atividade))
                .ReturnsAsync(true);

            var dummyResponse = new CalculoResponseDto { FaturamentoMensal = 12000m };
            _despesaMock
                .Setup(s => s.CalcularAsync(dto))
                .ReturnsAsync(dummyResponse);

            // Act
            var result = await ctrl.CalcularSimplesNacional(dto);

            // Assert
            var ok = Assert.IsType<OkObjectResult>(result);
            Assert.Equal(200, ok.StatusCode);
            Assert.Equal(dummyResponse, ok.Value);
        }
    }
}
