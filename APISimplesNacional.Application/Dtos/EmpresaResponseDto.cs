namespace APISimplesNacional.Application.Dtos
{
    public class EmpresaResponseDto
    {
        public string Nome { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal IrDependente { get; set; } = 0m;
    }
}
