using System.Reflection;
using APISimplesNacional.Application.Dtos;

namespace APISimplesNacional.Testes.Helpers
{
    public class CalculoHelperTests
    {
        // Tenta obter via reflection o tipo "APISimplesNacional.Application.Helpers.CalculoHelper"
        private static readonly Type? CalculoHelperType = Type.GetType(
            "APISimplesNacional.Application.Helpers.CalculoHelper, APISimplesNacional.Application"
        );

        // Obtém via reflection o método estático "CalcularINSS"
        private static readonly MethodInfo? CalcularInssMethod = CalculoHelperType?
            .GetMethod(
                "CalcularINSS",
                BindingFlags.Public | BindingFlags.Static,
                null,
                new[] { typeof(decimal), typeof(IEnumerable<TabelaINSSDto>) },
                null
            );

        // Obtém via reflection o método estático "CalcularIR"
        private static readonly MethodInfo? CalcularIrMethod = CalculoHelperType?
            .GetMethod(
                "CalcularIR",
                BindingFlags.Public | BindingFlags.Static,
                null,
                new[]
                {
                    typeof(decimal),
                    typeof(int),
                    typeof(IEnumerable<TabelaIRDto>),
                    typeof(decimal),
                    typeof(decimal),
                    typeof(decimal)
                },
                null
            );

        [Fact]
        public void CalcularINSS_DeveUsarFaixaCorreta()
        {
            // 1) Se o método não existir, falhar imediatamente
            if (CalcularInssMethod is null)
            {
                Assert.False(true,
                    "CalculoHelper.CalcularINSS não foi encontrado via reflection. " +
                    "Verifique se 'CalculoHelper' existe ou está no assembly correto.");
            }

            // Arrange
            var tabela = new List<TabelaINSSDto>
            {
                new TabelaINSSDto { Faixa = 1, LimiteInic =       0m, LimiteFin =    1518m, Aliquota =  7.5m, Deducao =   0m },
                new TabelaINSSDto { Faixa = 2, LimiteInic = 1518.01m, LimiteFin = 2793.88m, Aliquota =  9.0m, Deducao =   0m },
                new TabelaINSSDto { Faixa = 3, LimiteInic = 2793.89m, LimiteFin = 4190.83m, Aliquota = 12.0m, Deducao = 106.59m }
            };
            decimal salario = 3000m;

            // 2) Invoca CalcularINSS via reflection
            var descontoObj = CalcularInssMethod.Invoke(
                null,
                new object[] { salario, tabela }
            );
            Assert.NotNull(descontoObj);
            var desconto = (decimal)descontoObj!;

            // 3000 x 12% = 360.00 - 106.59 = 253.41
            Assert.Equal(253.41m, desconto);
        }

        [Fact]
        public void CalcularIR_DeveRetornarZeroSeBaseAbaixoIsencao()
        {
            // 1) Se o método não existir, falhar imediatamente
            if (CalcularIrMethod is null)
            {
                Assert.False(true,
                    "CalculoHelper.CalcularIR não foi encontrado via reflection. " +
                    "Verifique se 'CalculoHelper' existe ou está no assembly correto.");
            }

            // Arrange
            decimal salario = 2000m;
            int dependentes = 0;
            var irTabela = new List<TabelaIRDto>
            {
                new TabelaIRDto { Faixa = 1, LimiteInic =   0m,     LimiteFin = 3036m,      Aliquota =  0m,   VlrDeduzir =   0m },
                new TabelaIRDto { Faixa = 2, LimiteInic = 3036m,   LimiteFin = 3533.31m,   Aliquota =  7.5m, VlrDeduzir = 169.44m }
            };
            decimal inss = 100m;
            decimal dedDep = 189.59m;
            decimal isencao = 3036m;

            // 2) Invoca CalcularIR via reflection
            var irObj = CalcularIrMethod.Invoke(
                null,
                new object[] { salario, dependentes, irTabela, inss, dedDep, isencao }
            );
            Assert.NotNull(irObj);
            var ir = (decimal)irObj!;

            // BaseCalculo = 2000 - 100 - 0 = 1900 => abaixo isencao (3036)
            Assert.Equal(0m, ir);
        }

        [Fact]
        public void CalcularIR_DeveAplicarAliquotaCorretamente()
        {
            // 1) Se o método não existir, falhar imediatamente
            if (CalcularIrMethod is null)
            {
                Assert.False(true,
                    "CalculoHelper.CalcularIR não foi encontrado via reflection. " +
                    "Verifique se 'CalculoHelper' existe ou está no assembly correto.");
            }

            // Arrange
            decimal salario = 5000m;
            int dependentes = 1;
            var irTabela = new List<TabelaIRDto>
            {
                new TabelaIRDto { Faixa = 1, LimiteInic =      0m,      LimiteFin = 3036m,         Aliquota =  0m,    VlrDeduzir =   0m },
                new TabelaIRDto { Faixa = 2, LimiteInic = 3036.01m,  LimiteFin = 4688.85m,    Aliquota = 15m,   VlrDeduzir = 381.44m },
                new TabelaIRDto { Faixa = 3, LimiteInic = 4688.86m,  LimiteFin = decimal.MaxValue, Aliquota = 22.5m, VlrDeduzir = 662.77m }
            };
            decimal inss = 600m;
            decimal dedDep = 189.59m;
            decimal isencao = 0m;

            // baseCalculo = 5000 - 600 - 189.59 = 4210.41 => faixa 2:
            // ir = 4210.41 * 15% - 381.44 = 631.5615 - 381.44 = 250.12

            // 2) Invoca CalcularIR via reflection
            var irObj = CalcularIrMethod.Invoke(
                null,
                new object[] { salario, dependentes, irTabela, inss, dedDep, isencao }
            );
            Assert.NotNull(irObj);
            var ir = (decimal)irObj!;

            Assert.Equal(250.12m, ir);
        }
    }
}
