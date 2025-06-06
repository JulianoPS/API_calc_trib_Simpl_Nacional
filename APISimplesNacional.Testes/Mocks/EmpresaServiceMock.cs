using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Infra.Entidades;
using Moq;

namespace APISimplesNacional.Testes.Mocks
{
    public static class EmpresaServiceMock
    {
        public static Mock<IEmpresaService> Get(bool returnNull = false, bool isDefault = false)
        {
            var mock = new Mock<IEmpresaService>();
            if (returnNull)
                mock.Setup(s => s.ObterPorEmailOuCelularAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync((Empresas?)null);
            else
            {
                var empresa = new Empresas { Id = isDefault ? 1 : 2 };
                mock.Setup(s => s.ObterPorEmailOuCelularAsync(It.IsAny<string>(), It.IsAny<string>()))
                    .ReturnsAsync(empresa);
            }
            return mock;
        }
    }
}