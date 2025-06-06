using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Application.Services;
using APISimplesNacional.Infra.Entidades; // Para "Empresas"
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace APISimplesNacional.Testes.Servicos
{
    public class CalculoDespesaServiceTests
    {
        [Fact]
        public async Task CalcularAsync_DeveRetornarResponsePreenchido()
        {
            // 1) Mock de IEmpresaService
            var empMock = new Mock<IEmpresaService>();
            empMock.Setup(s => s.ObterPorEmailOuCelularAsync(
                            It.IsAny<string?>(), It.IsAny<string?>()))
                   .ReturnsAsync(new Empresas
                   {
                       Id = 1,
                       IrDependente = 189.59m,
                       IrVlrIsento = 3036m
                   });

            // 2) Mock de IAnexoIIIService (retorna AnexoIIIDto)
            var anexoIIIMock = new Mock<IAnexoIIIService>();
            anexoIIIMock.Setup(s => s.ObterPorEmailOuCelularAsync(
                                  It.IsAny<string?>(), It.IsAny<string?>()))
                        .ReturnsAsync(new List<AnexoIIIDto>
                        {
                            new AnexoIIIDto
                            {
                                Faixa = 1,
                                LimiteInic = 0,
                                LimiteFin = 9999,
                                Aliquota = 6m,
                                VlrDeduzir = 0
                            }
                        });

            // 3) Mock de IAnexoVService (retorna AnexoVDto)
            var anexoVMock = new Mock<IAnexoVService>();
            anexoVMock.Setup(s => s.ObterPorEmailOuCelularAsync(
                                 It.IsAny<string?>(), It.IsAny<string?>()))
                      .ReturnsAsync(new List<AnexoVDto>
                      {
                          new AnexoVDto
                          {
                              Faixa = 1,
                              LimiteInic = 0,
                              LimiteFin = 9999,
                              Aliquota = 15.5m,
                              VlrDeduzir = 0
                          }
                      });

            // 4) Mock de ITabelaINSSService (retorna TabelaINSSDto)
            var tabelaInssMock = new Mock<ITabelaINSSService>();
            tabelaInssMock.Setup(s => s.ObterPorEmpresaIdAsync(1))
                          .ReturnsAsync(new List<TabelaINSSDto>
                          {
                              new TabelaINSSDto
                              {
                                  Faixa = 1,
                                  LimiteInic = 0,
                                  LimiteFin = 9999,
                                  Aliquota = 10m,
                                  Deducao = 0m
                              }
                          });

            // 5) Mock de ITabelaIRService (retorna TabelaIRDto)
            var tabelaIrMock = new Mock<ITabelaIRService>();
            tabelaIrMock.Setup(s => s.ObterPorEmpresaIdAsync(1))
                        .ReturnsAsync(new List<TabelaIRDto>
                        {
                            new TabelaIRDto
                            {
                                Faixa      = 1,
                                LimiteInic = 0,
                                LimiteFin  = 9999,
                                Aliquota   = 0m,
                                VlrDeduzir = 0m
                            }
                        });

            // 6) Mock de ICalculoInssService
            var inssMock = new Mock<ICalculoInssService>();
            inssMock.Setup(s => s.CalcularInssSociosAsync(
                                It.IsAny<IEnumerable<SocioDto>>(),
                                It.IsAny<string?>(),
                                It.IsAny<string?>()))
                    .ReturnsAsync(new List<SocioResponseDto>
                    {
                        new SocioResponseDto
                        {
                            Nome           = "Test",
                            ValorProLabore = 1000m,
                            ValorINSS      = 110m,
                            ValorIR        = 0m,
                            ValorLiquido   = 890m // exemplo fixo
                        }
                    });

            inssMock.Setup(s => s.CalcularInssFuncionariosAsync(
                                It.IsAny<IEnumerable<FuncionarioDto>>(),
                                It.IsAny<string?>(),
                                It.IsAny<string?>()))
                    .ReturnsAsync(new List<FuncionarioResponseDto>
                    {
                        new FuncionarioResponseDto
                        {
                            Nome             = "Test",
                            ValorSalario     = 1000m,
                            ValorINSS        = 110m,
                            ValorIR          = 0m,
                            ValorLiquido     = 890m // exemplo fixo
                        }
                    });

            // 7) Mock de ICalculoIrService
            var irMock = new Mock<ICalculoIrService>();
            irMock.Setup(s => s.CalcularIrSociosAsync(
                              It.IsAny<IEnumerable<SocioDto>>(),
                              It.IsAny<string?>(),
                              It.IsAny<string?>()))
                  .ReturnsAsync(new List<SocioResponseDto>
                  {
                       new SocioResponseDto
                       {
                           Nome             = "Test",
                           ValorProLabore   = 1000m,
                           ValorIR          = 50m,
                           ValorINSS        = 110m,
                           ValorProLaboreAnual = 12000m,
                           ValorLiquido     = 840m // exemplo
                       }
                  });

            irMock.Setup(s => s.CalcularIrFuncionariosAsync(
                              It.IsAny<IEnumerable<FuncionarioDto>>(),
                              It.IsAny<string?>(),
                              It.IsAny<string?>()))
                  .ReturnsAsync(new List<FuncionarioResponseDto>
                  {
                       new FuncionarioResponseDto
                       {
                           Nome             = "Test",
                           ValorSalario     = 1000m,
                           ValorIR          = 50m,
                           ValorINSS        = 110m,
                           ValorSalarioAnual = 13533.33m,
                           ValorLiquido     = 840m // exemplo
                       }
                  });

            // 8) Criar instância de CalculoDespesaService, fornecendo **TUDO** que o construtor exige:
            var service = new CalculoDespesaService(
                empMock.Object,
                anexoIIIMock.Object,
                anexoVMock.Object,
                tabelaInssMock.Object,   // <--- novo
                tabelaIrMock.Object,     // <--- novo
                inssMock.Object,
                irMock.Object
            );

            // 9) Montar a requisição
            var request = new CalculoRequestDto
            {
                Celular = "",
                Email = "",
                FaturamentoMensal = 1000m,
                DespesasFixas = new DespesasFixasDto
                {
                    Contador = 100m,
                    AluguelSala = 200m,
                    Internet = 50m,
                    AguaEenergia = 150m
                },
                Socios = new List<SocioDto>
                {
                    new SocioDto
                    {
                        Nome             = "Test",
                        ValorProLabore   = 1000m,
                        NumeroDependentes = 0
                    }
                },
                Funcionarios = new List<FuncionarioDto>
                {
                    new FuncionarioDto
                    {
                        Nome             = "Test",
                        ValorSalario     = 1000m,
                        NumeroDependentes = 0
                    }
                }
            };

            // 10) Executa o método a testar
            var result = await service.CalcularAsync(request);

            // 11) Validações básicas
            Assert.Equal(1000m, result.FaturamentoMensal);
            Assert.Equal(12000m, result.FaturamentoAnual); // 1.000 * 12
            Assert.Single(result.Socios);
            Assert.Single(result.Funcionarios);
            Assert.True(result.DAS > 0);
        }
    }
}
