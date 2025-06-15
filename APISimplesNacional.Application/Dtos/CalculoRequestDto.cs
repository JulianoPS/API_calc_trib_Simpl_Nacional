using System.ComponentModel.DataAnnotations;

namespace APISimplesNacional.Application.Dtos
{
    public class CalculoRequestDto
    {
        
        public string Atividade { get; set; } = string.Empty;
        public string? Celular { get; set; }
        public string? Email { get; set; }
        public decimal FaturamentoMensal { get; set; }
        public DespesasFixasDto DespesasFixas { get; set; } = new();
        public IEnumerable<SocioDto> Socios { get; set; } = new List<SocioDto>();
        public IEnumerable<FuncionarioDto> Funcionarios { get; set; } = new List<FuncionarioDto>();
    }


}