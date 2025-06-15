using Moq;
using APISimplesNacional.Application.Services;
using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Infra.Entidades;
using AutoMapper;

namespace APISimplesNacional.Testes.Servicos
{

    public class EmpresaServiceTest
    {

        private readonly Mock<IMapper> _mapperMock;

        public EmpresaServiceTest()
        {
            // Cria um mock vazio de IMapper
            _mapperMock = new Mock<IMapper>();
        }

        [Fact]
        public async Task CriarEmpresaComTabelasAsync_DeveLancarExcecao_QuandoFalhaAoSalvar()
        {
            // Arrange
            var mockEmpresaRepositorio = new Mock<IEmpresaRepositorio>();
            var mockClonagemRepositorio = new Mock<IClonagemRepositorio>();

            // Simula erro ao inserir
            mockEmpresaRepositorio
                .Setup(r => r.AdicionarAsync(It.IsAny<Empresas>()))
                .ThrowsAsync(new Exception("Erro ao inserir no banco"));

            var service = new EmpresaService(
                mockEmpresaRepositorio.Object,
                mockClonagemRepositorio.Object,
                _mapperMock.Object     
            );


            mockEmpresaRepositorio
                .Setup(r => r.AdicionarAsync(It.IsAny<Empresas>()))
                .ThrowsAsync(new Exception("Erro ao inserir no banco"));

            var dto = new CriarEmpresaDto
            {
                Nome = "Empresa Teste",
                Email = "teste@email.com",
                Celular = "11999999999",
                IrDependente = 189.59m,
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => service.CriarEmpresaComTabelasAsync(dto));
            Assert.Equal("Erro ao inserir no banco", ex.Message);
        }

        [Fact]
        public async Task CriarEmpresaComTabelasAsync_DeveLancarExcecao_SeEmailOuCelularJaExistirem()
        {
            // Arrange
            var mockEmpresaRepositorio = new Mock<IEmpresaRepositorio>();
            var mockClonagemRepositorio = new Mock<IClonagemRepositorio>();

            var empresaExistente = new Empresas
            {
                Id = 2,
                Nome = "Empresa Existente",
                Email = "teste@email.com",
                Celular = "11999999999"
            };

            // Simula que já existe uma empresa com o mesmo e-mail ou celular
            mockEmpresaRepositorio
                .Setup(r => r.ObterPorEmailOuCelularAsync(empresaExistente.Email, empresaExistente.Celular))
                .ReturnsAsync(empresaExistente);

            var service = new EmpresaService(
                mockEmpresaRepositorio.Object,
                mockClonagemRepositorio.Object,
                _mapperMock.Object      
            );

            var dto = new CriarEmpresaDto
            {
                Nome = "Nova Empresa",
                Email = "teste@email.com",
                Celular = "11999999999",
                IrDependente = 189.59m
            };

            // Act & Assert
            var ex = await Assert.ThrowsAsync<InvalidOperationException>(() =>
                service.CriarEmpresaComTabelasAsync(dto));

            Assert.Equal("Já existe uma empresa com este e-mail ou celular.", ex.Message);
        }
    }
}
