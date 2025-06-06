namespace APISimplesNacional.Application.Dtos
{
    public class FuncionarioResponseDto
    {
        public string Nome { get; set; } = string.Empty;
        public decimal ValorSalario { get; set; }
        public decimal ValorSalarioAnual { get; set; }
        public decimal MediaMensal { get; set; }
        public int NumeroDependentes { get; set; }
        public decimal ValorINSS { get; set; }
        public decimal ValorIR { get; set; }

        /// <summary>
        /// Valor líquido do salário: ValorSalario – ValorINSS – ValorIR
        /// </summary>
        public decimal ValorLiquido { get; set; }
    }
}
