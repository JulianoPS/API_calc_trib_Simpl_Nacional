using APISimplesNacional.API;
using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace APISimplesNacional.Testes.Servicos
{
    public class TabelaIRControllerTestes
    {
        private readonly Mock<ITabelaIRService> _serviceMock;
        private readonly TabelaIRController _controller;

        public TabelaIRControllerTestes()
        {
            _serviceMock = new Mock<ITabelaIRService>();
            _controller = new TabelaIRController(_serviceMock.Object);
        }

        [Fact]
        public async Task Obter_DeveRetornarOkComDados()
        {
            // Arrange
            var resultadoEsperado = new List<TabelaIRDto>
            {
                new TabelaIRDto { Faixa = 1, LimiteInic = 0, LimiteFin = 1000, Aliquota = 0.1m, VlrDeduzir = 0 }
            };

            _serviceMock.Setup(s => s.ObterPorEmailOuCelularAsync("email@teste.com", "123"))
                        .ReturnsAsync(resultadoEsperado);

            // Act
            var result = await _controller.Obter("email@teste.com", "123");

            // Assert
            var okResult = Assert.IsType<OkObjectResult>(result);
            var dados = Assert.IsAssignableFrom<List<TabelaIRDto>>(okResult.Value);
            Assert.Single(dados);
        }

        [Fact]
        public async Task Obter_DeveRetornarBadRequest_EmCasoDeErro()
        {
            // Arrange
            _serviceMock.Setup(s => s.ObterPorEmailOuCelularAsync(It.IsAny<string>(), It.IsAny<string>()))
                        .ThrowsAsync(new Exception("Erro ao obter dados"));

            // Act
            var result = await _controller.Obter("invalido@teste.com", "999");

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(result);
            Assert.Equal(400, badRequest.StatusCode);
            Assert.Contains("Erro", badRequest.Value?.ToString());
        }

        
        [Fact]
        public async Task Atualizar_DeveRetornarNoContent()
        {
            // Arrange
            var dados = new List<TabelaIRDto>
            {
                new TabelaIRDto { Faixa = 1, LimiteInic = 0, LimiteFin = 1000, Aliquota = 0.1m, VlrDeduzir = 0 }
            };

            _serviceMock.Setup(s => s.AtualizarAsync("email@teste.com", "123", dados))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Atualizar("email@teste.com", "123", dados);

            // Assert
            Assert.IsType<NoContentResult>(result);
        }

        [Fact]
        public async Task Atualizar_DeveRetornarBadRequest_QuandoInvalidOperationExceptionForLancada()
        {
            // Arrange
            var dtos = new List<TabelaIRDto>
        {
            new TabelaIRDto
            {
                Faixa = 1,
                LimiteInic = 0,
                LimiteFin = 2000,
                Aliquota = 0.05m,
                VlrDeduzir = 0
            }
        };

            _serviceMock.Setup(s => s.AtualizarAsync("email@teste.com", "123", It.IsAny<IEnumerable<TabelaIRDto>>()))
                        .ThrowsAsync(new InvalidOperationException("Dados inválidos"));

            // Act
            var resultado = await _controller.Atualizar("email@teste.com", "123", dtos);

            // Assert
            var badRequest = Assert.IsType<BadRequestObjectResult>(resultado);
            Assert.Contains("Dados inválidos", badRequest.Value!.ToString());
        }


    }
}
