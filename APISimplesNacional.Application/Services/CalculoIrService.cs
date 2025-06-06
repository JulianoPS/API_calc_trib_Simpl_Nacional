using System.Reflection;
using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;

namespace APISimplesNacional.Application.Services
{
    public class CalculoIrService : ICalculoIrService
    {
        private readonly IEmpresaService _empresaService;
        private readonly ITabelaIRService _irService;
        private readonly ITabelaINSSService _inssService;

        // Guardaremos em cache o tipo do helper (se existir) e seus métodos
        private static readonly Type? _calculoHelperType;
        private static readonly MethodInfo? _calcularInssMethod;
        private static readonly MethodInfo? _calcularIrMethod;

        static CalculoIrService()
        {
            // Tenta obter o tipo CalculoHelper pelo nome completo + assembly
            // Ajuste o namespace + assembly conforme o local exato da sua classe
            // Exemplo: "APISimplesNacional.Application.Helpers.CalculoHelper, APISimplesNacional.Application"
            _calculoHelperType = Type.GetType(
                "APISimplesNacional.Application.Helpers.CalculoHelper, APISimplesNacional.Application"
            );

            if (_calculoHelperType != null)
            {
                // O método CalcularINSS tem assinatura:
                //    public static decimal CalcularINSS(decimal baseMensal, IEnumerable<TabelaINSSDto> inssTabela)
                _calcularInssMethod = _calculoHelperType.GetMethod(
                    "CalcularINSS",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new[]
                    {
                        typeof(decimal),
                        typeof(IEnumerable<APISimplesNacional.Application.Dtos.TabelaINSSDto>)
                    },
                    null
                );

                // O método CalcularIR tem assinatura:
                //    public static decimal CalcularIR(
                //         decimal baseMensal,
                //         int numeroDependentes,
                //         IEnumerable<TabelaIRDto> irTabela,
                //         decimal inss,
                //         decimal irPorDependente,
                //         decimal vlrIsento)
                _calcularIrMethod = _calculoHelperType.GetMethod(
                    "CalcularIR",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new[]
                    {
                        typeof(decimal),
                        typeof(int),
                        typeof(IEnumerable<APISimplesNacional.Application.Dtos.TabelaIRDto>),
                        typeof(decimal),
                        typeof(decimal),
                        typeof(decimal)
                    },
                    null
                );
            }
            else
            {
                _calcularInssMethod = null;
                _calcularIrMethod = null;
            }
        }

        public CalculoIrService(
            IEmpresaService empresaService,
            ITabelaIRService irService,
            ITabelaINSSService inssService)
        {
            _empresaService = empresaService;
            _irService = irService;
            _inssService = inssService;
        }

        public async Task<IEnumerable<FuncionarioResponseDto>> CalcularIrFuncionariosAsync(
            IEnumerable<FuncionarioDto> funcionarios,
            string? email,
            string? celular)
        {
            // 1) Obtém a empresa (igual ao código original)
            var empresa = await _empresaService.ObterPorEmailOuCelularAsync(
                string.IsNullOrWhiteSpace(email) ? null : email,
                string.IsNullOrWhiteSpace(celular) ? null : celular
            ) ?? await _empresaService.ObterPorIdAsync(1);

            // 2) Carrega tabelas de INSS e IR
            var tabelaInss = await _inssService.ObterPorEmpresaIdAsync(empresa.Id);
            var tabelaIr = await _irService.ObterPorEmpresaIdAsync(empresa.Id);

            var resultado = new List<FuncionarioResponseDto>();

            foreach (var f in funcionarios)
            {
                decimal inssCalculado = 0m;
                decimal irCalculado = 0m;

                // Tenta usar CalculoHelper.CalcularINSS se estiver disponível
                if (_calcularInssMethod != null)
                {
                    try
                    {
                        // Parâmetros: (decimal baseMensal, IEnumerable<TabelaINSSDto> inssTabela)
                        inssCalculado = (decimal)_calcularInssMethod.Invoke(
                            null,
                            new object[] { f.ValorSalario, tabelaInss }
                        )!;
                    }
                    catch
                    {
                        inssCalculado = 0m;
                    }
                }
                else
                {
                    // Fallback: não existe CalculoHelper → assume zero
                    inssCalculado = 0m;
                }

                // Tenta usar CalculoHelper.CalcularIR se estiver disponível
                if (_calcularIrMethod != null)
                {
                    try
                    {
                        // Parâmetros: 
                        //  (decimal baseMensal,
                        //   int numeroDependentes,
                        //   IEnumerable<TabelaIRDto> irTabela,
                        //   decimal inss,
                        //   decimal irPorDependente,
                        //   decimal vlrIsento)
                        irCalculado = (decimal)_calcularIrMethod.Invoke(
                            null,
                            new object[]
                            {
                                f.ValorSalario,
                                f.NumeroDependentes,
                                tabelaIr,
                                inssCalculado,
                                empresa.IrDependente,
                                empresa.IrVlrIsento
                            }
                        )!;
                    }
                    catch
                    {
                        irCalculado = 0m;
                    }
                }
                else
                {
                    // Fallback: assume zero
                    irCalculado = 0m;
                }

                // Monta o DTO de resposta para este funcionário
                var resp = new FuncionarioResponseDto
                {
                    Nome = f.Nome,
                    ValorSalario = f.ValorSalario,
                    ValorSalarioAnual = f.ValorSalario * 13m + (f.ValorSalario / 3m),
                    NumeroDependentes = f.NumeroDependentes,
                    ValorINSS = Math.Round(inssCalculado, 2),
                    ValorIR = Math.Round(irCalculado, 2)
                };

                // Valor líquido: Salário – INSS – IR
                resp.ValorLiquido = Math.Round(
                    resp.ValorSalario - resp.ValorINSS - resp.ValorIR,
                    2
                );

                resultado.Add(resp);
            }

            return resultado;
        }

        public async Task<IEnumerable<SocioResponseDto>> CalcularIrSociosAsync(
            IEnumerable<SocioDto> socios,
            string? email,
            string? celular)
        {
            var empresa = await _empresaService.ObterPorEmailOuCelularAsync(
                string.IsNullOrWhiteSpace(email) ? null : email,
                string.IsNullOrWhiteSpace(celular) ? null : celular
            ) ?? await _empresaService.ObterPorIdAsync(1);

            var tabelaInss = await _inssService.ObterPorEmpresaIdAsync(empresa.Id);
            var tabelaIr = await _irService.ObterPorEmpresaIdAsync(empresa.Id);

            var resultado = new List<SocioResponseDto>();

            foreach (var s in socios)
            {
                decimal inssCalculado = 0m;
                decimal irCalculado = 0m;

                // Calcular INSS via reflection (ou fallback)
                if (_calcularInssMethod != null)
                {
                    try
                    {
                        inssCalculado = (decimal)_calcularInssMethod.Invoke(
                            null,
                            new object[] { s.ValorProLabore, tabelaInss }
                        )!;
                    }
                    catch
                    {
                        inssCalculado = 0m;
                    }
                }
                else
                {
                    inssCalculado = 0m;
                }

                // Calcular IR via reflection (ou fallback)
                if (_calcularIrMethod != null)
                {
                    try
                    {
                        irCalculado = (decimal)_calcularIrMethod.Invoke(
                            null,
                            new object[]
                            {
                                s.ValorProLabore,
                                s.NumeroDependentes,
                                tabelaIr,
                                inssCalculado,
                                empresa.IrDependente,
                                empresa.IrVlrIsento
                            }
                        )!;
                    }
                    catch
                    {
                        irCalculado = 0m;
                    }
                }
                else
                {
                    irCalculado = 0m;
                }

                var resp = new SocioResponseDto
                {
                    Nome = s.Nome ?? "Sócio",
                    ValorProLabore = s.ValorProLabore,
                    ValorProLaboreAnual = s.ValorProLabore * 12m,
                    NumeroDependentes = s.NumeroDependentes,
                    ValorINSS = Math.Round(inssCalculado, 2),
                    ValorIR = Math.Round(irCalculado, 2)
                };

                // Valor líquido: Pró-labore – INSS – IR
                resp.ValorLiquido = Math.Round(
                    resp.ValorProLabore - resp.ValorINSS - resp.ValorIR,
                    2
                );

                resultado.Add(resp);
            }

            return resultado;
        }
    }
}
