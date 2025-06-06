namespace APISimplesNacional.Application.Dtos
{
    public class SocioDto
    {
        public string? Nome { get; set; } = string.Empty;
        public decimal ValorProLabore { get; set; }
        public int NumeroDependentes { get; set; }
        public bool FGTS { get; set; } = false;
    }
}