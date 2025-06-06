using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Services;
using APISimplesNacional.Domain.Repositories;
using APISimplesNacional.Infra.Entidades;
using Moq;
using Xunit;

namespace APISimplesNacional.Testes.Servicos
{
    public class AnexoVServiceTests
    {
        private readonly Mock<IAnexoVRepositorio> _repoMock;
        private readonly AnexoVService _service;

        public AnexoVServiceTests()
        {
            _repoMock = new Mock<IAnexoVRepositorio>();
            var empMock = Mocks.EmpresaServiceMock.Get();
            _service = new AnexoVService(empMock.Object, _repoMock.Object);
        }

        [Fact]
        public async Task ObterPorEmailOuCelularAsync_ShouldReturnDefault_WhenEmpresaNotFound()
        {
            var empNull = Mocks.EmpresaServiceMock.Get(returnNull: true);
            empNull.Setup(s => s.ObterPorIdAsync(1))
                   .ReturnsAsync(new Empresas { Id = 1 });

            var serv = new AnexoVService(empNull.Object, _repoMock.Object);
            var defaultEnt = new List<AnexoV> { new AnexoV { IdEmpresa = 1, Faixa = 1, LimiteInic = 0, LimiteFin = 100, Aliquota = 0.075m, VlrDeduzir = 0 } };
            _repoMock.Setup(r => r.ObterPorEmpresaIdAsync(1)).ReturnsAsync(defaultEnt);

            var result = await serv.ObterPorEmailOuCelularAsync("e", "c");

            Assert.Single(result);
            Assert.Equal(1, result.First().Faixa);
        }

        [Fact]
        public async Task AtualizarAsync_ShouldThrow_WhenEmpresaDefault()
        {
            var empDefault = Mocks.EmpresaServiceMock.Get(isDefault: true);
            var serv = new AnexoVService(empDefault.Object, _repoMock.Object);
            await Assert.ThrowsAsync<InvalidOperationException>(
                () => serv.AtualizarAsync("e", "c", new List<AnexoVDto>())
            );
        }

        [Fact]
        public async Task AtualizarAsync_ShouldCallRepository_WhenValid()
        {
            var lista = new List<AnexoVDto> { new AnexoVDto { Faixa = 2, LimiteInic = 0, LimiteFin = 200, Aliquota = 0.15m, VlrDeduzir = 50 } };
            await _service.AtualizarAsync("e", "c", lista);
            _repoMock.Verify(
                r => r.AtualizarAsync(
                    It.Is<List<AnexoV>>(l => l.Count == 1 && l[0].Faixa == 2 && l[0].VlrDeduzir == 50)
                ), Times.Once
            );
        }
    }
}