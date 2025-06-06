using System.Reflection;
using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Domain.Interfaces;

namespace APISimplesNacional.Application.Services
{
    public class CalculoDespesaService : ICalculoDespesaService
    {
        private readonly IEmpresaService _empresaService;
        private readonly IAnexoIIIService _anexo3Service;
        private readonly IAnexoVService _anexoVService;
        private readonly ITabelaINSSService _tabelaInssService;
        private readonly ITabelaIRService _tabelaIrService;
        private readonly ICalculoInssService _inssService;
        private readonly ICalculoIrService _irService;

        private static readonly Type? _calculoHelperType;
        private static readonly MethodInfo? _executarCalculoCompletoMethod;

        static CalculoDespesaService()
        {
            _calculoHelperType = Type.GetType(
                "APISimplesNacional.Application.Helpers.CalculoHelper, APISimplesNacional.Application"
            );

            if (_calculoHelperType != null)
            {
                _executarCalculoCompletoMethod = _calculoHelperType.GetMethod(
                    "ExecutarCalculoCompleto",
                    BindingFlags.Public | BindingFlags.Static,
                    null,
                    new[]
                    {
                        typeof(CalculoRequestDto),
                        typeof(APISimplesNacional.Infra.Entidades.Empresas),
                        typeof(IEnumerable<TabelaINSSDto>),
                        typeof(IEnumerable<TabelaIRDto>),
                        typeof(IEnumerable<AnexoIIIDto>),
                        typeof(IEnumerable<AnexoVDto>)
                    },
                    null
                );
            }
        }

        public CalculoDespesaService(
            IEmpresaService empresaService,
            IAnexoIIIService anexoIIIService,
            IAnexoVService anexoVService,
            ITabelaINSSService tabelaInssService,
            ITabelaIRService tabelaIrService,
            ICalculoInssService inssService,
            ICalculoIrService irService)
        {
            _empresaService = empresaService;
            _anexo3Service = anexoIIIService;
            _anexoVService = anexoVService;
            _tabelaInssService = tabelaInssService;
            _tabelaIrService = tabelaIrService;
            _inssService = inssService;
            _irService = irService;
        }

        public async Task<CalculoResponseDto> CalcularAsync(CalculoRequestDto req)
        {
            // 1) Determina empresa
            var emp = await _empresaService.ObterPorEmailOuCelularAsync(
                string.IsNullOrWhiteSpace(req.Email) ? null : req.Email,
                string.IsNullOrWhiteSpace(req.Celular) ? null : req.Celular
            ) ?? await _empresaService.ObterPorIdAsync(1);

            // 2) Carrega tabelas Anexo III e V
            var tabIII = await _anexo3Service.ObterPorEmailOuCelularAsync(req.Email!, req.Celular!);
            var tabV = await _anexoVService.ObterPorEmailOuCelularAsync(req.Email!, req.Celular!);

            // 3) Carrega tabelas de INSS e IR
            var tabINSS = await _tabelaInssService.ObterPorEmpresaIdAsync(emp.Id);
            var tabIR = await _tabelaIrService.ObterPorEmpresaIdAsync(emp.Id);

            // 4) Tenta chamar o helper completo via reflection
            if (_executarCalculoCompletoMethod != null)
            {
                try
                {
                    object? resultado = _executarCalculoCompletoMethod.Invoke(
                        null,
                        new object[] { req, emp, tabINSS, tabIR, tabIII, tabV }
                    );
                    return (CalculoResponseDto)resultado!;
                }
                catch
                {
                    // Se der qualquer erro, cai no stub abaixo
                }
            }

            // 5) Fallback stub (retorna valores padrão)
            var stub = new CalculoResponseDto
            {
                FaturamentoMensal = req.FaturamentoMensal,
                FaturamentoAnual = 0m,
                FolhaSalarios = 0m,
                FatorR = 0m,
                Anexo = string.Empty,
                AliquotaNominal = 0m,
                AliquotaEfetiva = 0m,
                DAS = 0m,
                ValorLiquidoMensal = 0m,
                DespesasFixas = req.DespesasFixas,
                DespesasFixasAnual = new DespesasFixasAnualDto(),
                Socios = new List<SocioResponseDto>(),
                Funcionarios = new List<FuncionarioResponseDto>(),
                Encargos = new EncargosDto(),
                EncargosMEI = new EncargosMEIDto()
            };
            await Task.CompletedTask;
            return stub;
        }
    }
}
