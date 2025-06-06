namespace APISimplesNacional.Application.Dtos
{
    public class FuncionarioDto
    {
        public string Nome { get; set; } = string.Empty;
        public decimal ValorSalario { get; set; }
        public int NumeroDependentes { get; set; }
    }
}