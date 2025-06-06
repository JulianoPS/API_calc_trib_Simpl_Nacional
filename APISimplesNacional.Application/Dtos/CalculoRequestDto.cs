using System.ComponentModel.DataAnnotations;

namespace APISimplesNacional.Application.Dtos
{
    public class CalculoRequestDto
    {
        [Required(ErrorMessage = "A atividade é obrigatória.")]
        public string Atividade { get; set; } = string.Empty;
        public string? Celular { get; set; }
        public string? Email { get; set; }

        [Required(ErrorMessage = "Faturamento mensal é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "Faturamento mensal é obrigatório e deve ser maior que zero.")] 
        public decimal FaturamentoMensal { get; set; }
        public DespesasFixasDto DespesasFixas { get; set; } = new();
        public IEnumerable<SocioDto> Socios { get; set; } = new List<SocioDto>();
        public IEnumerable<FuncionarioDto> Funcionarios { get; set; } = new List<FuncionarioDto>();
    }
}