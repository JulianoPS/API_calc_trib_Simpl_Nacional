namespace APISimplesNacional.Application.Dtos
{
    public class SocioResponseDto
    {
        public string Nome { get; set; } = string.Empty;
        public decimal ValorProLabore { get; set; }
        public decimal ValorProLaboreAnual { get; set; }
        public int NumeroDependentes { get; set; }
        public decimal ValorINSS { get; set; }
        public decimal ValorIR { get; set; }

        /// <summary>
        /// Valor líquido do pró-labore: ValorProLabore – ValorINSS – ValorIR
        /// </summary>
        public decimal ValorLiquido { get; set; }
    }
}
