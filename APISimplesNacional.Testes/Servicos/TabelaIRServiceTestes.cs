using APISimplesNacional.Application.Services;
using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Domain.Repositories;
using APISimplesNacional.Infra.Entidades;
using Moq;
using Xunit;
using APISimplesNacional.Application.Dtos;

namespace APISimplesNacional.Testes.Servicos
{
    public class TabelaIRServiceTestes
    {
        private readonly Mock<IEmpresaService> _empresaServiceMock;
        private readonly Mock<ITabelaIRRepositorio> _tabelaRepositorioMock;
        private readonly TabelaIRService _service;

        public TabelaIRServiceTestes()
        {
            _empresaServiceMock = new Mock<IEmpresaService>();
            _tabelaRepositorioMock = new Mock<ITabelaIRRepositorio>();
            _service = new TabelaIRService(_empresaServiceMock.Object, _tabelaRepositorioMock.Object);
        }

        [Fact]
        public async Task ObterPorEmailOuCelularAsync_DeveRetornarTabelaDaEmpresa()
        {
            // Arrange
            var empresa = new Empresas { Id = 2, Email = "teste@empresa.com", Celular = "123" };
            var tabelaIR = new List<TabelaIR>
            {
                new TabelaIR { Id = 1, IdEmpresa = 2, Faixa = 1, LimiteInic = 0, LimiteFin = 1000, Aliquota = 0.1m, VlrDeduzir = 0 }
            };

            _empresaServiceMock.Setup(s => s.ObterPorEmailOuCelularAsync("teste@empresa.com", "123"))
                               .ReturnsAsync(empresa);
            _tabelaRepositorioMock.Setup(r => r.ObterPorEmpresaIdAsync(2))
                                  .ReturnsAsync(tabelaIR);

            // Act
            var resultado = await _service.ObterPorEmailOuCelularAsync("teste@empresa.com", "123");

            // Assert
            Assert.Single(resultado);
            Assert.Equal(1, resultado.First().Faixa);
        }

        [Fact]
        public async Task ObterPorEmailOuCelularAsync_DeveBuscarEmpresaPadrao_SeNaoEncontrarEmpresa()
        {
            // Arrange
            var empresaPadrao = new Empresas { Id = 1, Email = "padrao@empresa.com", Celular = "000" };
            var tabelaIR = new List<TabelaIR>
            {
                new TabelaIR { Id = 1, IdEmpresa = 1, Faixa = 1, LimiteInic = 0, LimiteFin = 2000, Aliquota = 0.05m, VlrDeduzir = 0 }
            };

            _empresaServiceMock.Setup(s => s.ObterPorEmailOuCelularAsync("naoexiste@empresa.com", "000"))
                               .ReturnsAsync((Empresas?)null);
            _empresaServiceMock.Setup(s => s.ObterPorIdAsync(1))
                               .ReturnsAsync(empresaPadrao);
            _tabelaRepositorioMock.Setup(r => r.ObterPorEmpresaIdAsync(1))
                                  .ReturnsAsync(tabelaIR);

            // Act
            var resultado = await _service.ObterPorEmailOuCelularAsync("naoexiste@empresa.com", "000");

            // Assert
            Assert.Single(resultado);
            Assert.Equal(1, resultado.First().Faixa);
        }

        [Fact]
        public async Task AtualizarAsync_DeveChamarRepositorioComDadosConvertidos()
        {
            // Arrange
            var empresa = new Empresas { Id = 2, Email = "teste@empresa.com", Celular = "123" };
            var dtos = new List<TabelaIRDto>
            {
                new TabelaIRDto { Faixa = 1, LimiteInic = 0, LimiteFin = 1000, Aliquota = 0.1m, VlrDeduzir = 0 }
            };

            _empresaServiceMock.Setup(s => s.ObterPorEmailOuCelularAsync("teste@empresa.com", "123"))
                               .ReturnsAsync(empresa);

            _tabelaRepositorioMock.Setup(r => r.AtualizarAsync(It.IsAny<List<TabelaIR>>()))
                                  .Returns(Task.CompletedTask);

            // Act
            await _service.AtualizarAsync("teste@empresa.com", "123", dtos);

            // Assert
            _tabelaRepositorioMock.Verify(r => r.AtualizarAsync(It.Is<List<TabelaIR>>(lista =>
                lista.Count == 1 &&
                lista[0].Faixa == 1 &&
                lista[0].IdEmpresa == 2
            )), Times.Once);
        }

        [Fact]
        public async Task AtualizarAsync_DeveLancarExcecao_SeEmpresaNaoForEncontrada()
        {
            // Arrange
            _empresaServiceMock.Setup(s => s.ObterPorEmailOuCelularAsync(It.IsAny<string>(), It.IsAny<string>()))
                               .ReturnsAsync((Empresas?)null);

            var dtos = new List<TabelaIRDto>();

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.AtualizarAsync("email", "celular", dtos));
            Assert.Equal("Empresa não encontrada. Cadastre a empresa primeiro.", ex.Message);
        }

        [Fact]
        public async Task AtualizarAsync_DeveLancarExcecao_SeEmpresaForPadrao()
        {
            // Arrange
            var empresa = new Empresas { Id = 1, Email = "padrao@empresa.com", Celular = "000" };

            _empresaServiceMock.Setup(s => s.ObterPorEmailOuCelularAsync("padrao@empresa.com", "000"))
                               .ReturnsAsync(empresa);

            var dtos = new List<TabelaIRDto>();

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                _service.AtualizarAsync("padrao@empresa.com", "000", dtos));
            Assert.Equal("Não é permitido alterar os dados da empresa padrão.", ex.Message);
        }
    }
}
