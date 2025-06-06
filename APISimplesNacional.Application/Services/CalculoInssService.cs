// APISimplesNacional.Application/Services/CalculoInssService.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Application.Services
{
    public class CalculoInssService : ICalculoInssService
    {
        private readonly IEmpresaService _empresaService;
        private readonly ITabelaINSSService _tabelaInssService;

        // Reflection: pegar método CalcularINSS do helper
        private static readonly Type? _calculoHelperType;
        private static readonly MethodInfo? _calcularInssMethod;

        static CalculoInssService()
        {
            _calculoHelperType = Type.GetType(
                "APISimplesNacional.Application.Helpers.CalculoHelper, APISimplesNacional.Application"
            );

            if (_calculoHelperType != null)
            {
                _calcularInssMethod = _calculoHelperType.GetMethod(
                    "CalcularINSS",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new[] { typeof(decimal), typeof(IEnumerable<TabelaINSSDto>) },
                    null
                );
            }
        }

        public CalculoInssService(
            IEmpresaService empresaService,
            ITabelaINSSService tabelaInssService)
        {
            _empresaService = empresaService;
            _tabelaInssService = tabelaInssService;
        }

        public async Task<IEnumerable<SocioResponseDto>> CalcularInssSociosAsync(
            IEnumerable<SocioDto> socios, string? email, string? celular)
        {
            var empresa = await _empresaService.ObterPorEmailOuCelularAsync(
                                string.IsNullOrWhiteSpace(email) ? null : email,
                                string.IsNullOrWhiteSpace(celular) ? null : celular
                             ) ?? await _empresaService.ObterPorIdAsync(1);

            var tabelaInss = await _tabelaInssService.ObterPorEmpresaIdAsync(empresa.Id);

            var resultado = new List<SocioResponseDto>();

            foreach (var s in socios)
            {
                decimal valorInss = 0m;

                if (_calcularInssMethod != null)
                {
                    try
                    {
                        valorInss = (decimal)_calcularInssMethod.Invoke(
                            null,
                            new object[] { s.ValorProLabore, tabelaInss }
                        )!;
                    }
                    catch
                    {
                        valorInss = 0m;
                    }
                }
                else
                {
                    valorInss = 0m;
                }

                resultado.Add(new SocioResponseDto
                {
                    Nome = s.Nome ?? "Sócio",
                    ValorProLabore = s.ValorProLabore,
                    ValorProLaboreAnual = s.ValorProLabore * 12m,
                    NumeroDependentes = s.NumeroDependentes,
                    ValorINSS = Math.Round(valorInss, 2),
                    ValorIR = 0m,       // IR fica por conta do CalculoIrService
                    ValorLiquido = 0m        // este campo será ajustado no CalculoIrService
                });
            }

            return resultado;
        }

        public async Task<IEnumerable<FuncionarioResponseDto>> CalcularInssFuncionariosAsync(
            IEnumerable<FuncionarioDto> funcionarios, string? email, string? celular)
        {
            var empresa = await _empresaService.ObterPorEmailOuCelularAsync(
                                string.IsNullOrWhiteSpace(email) ? null : email,
                                string.IsNullOrWhiteSpace(celular) ? null : celular
                             ) ?? await _empresaService.ObterPorIdAsync(1);

            var tabelaInss = await _tabelaInssService.ObterPorEmpresaIdAsync(empresa.Id);

            var resultado = new List<FuncionarioResponseDto>();

            foreach (var f in funcionarios)
            {
                decimal valorInss = 0m;

                if (_calcularInssMethod != null)
                {
                    try
                    {
                        valorInss = (decimal)_calcularInssMethod.Invoke(
                            null,
                            new object[] { f.ValorSalario, tabelaInss }
                        )!;
                    }
                    catch
                    {
                        valorInss = 0m;
                    }
                }
                else
                {
                    valorInss = 0m;
                }

                resultado.Add(new FuncionarioResponseDto
                {
                    Nome = f.Nome,
                    ValorSalario = f.ValorSalario,
                    ValorSalarioAnual = f.ValorSalario * 13m + (f.ValorSalario / 3m),
                    NumeroDependentes = f.NumeroDependentes,
                    ValorINSS = Math.Round(valorInss, 2),
                    ValorIR = 0m,      // IR ficará a cargo de CalculoIrService
                    ValorLiquido = 0m       // ajustado depois
                });
            }

            return resultado;
        }
    }
}
