namespace APISimplesNacional.Application.Dtos
{
    public class CalculoResponseDto
    {
        public decimal FaturamentoMensal { get; set; }
        public decimal FaturamentoAnual { get; set; }
        public decimal FolhaSalarios { get; set; }
        public decimal FatorR { get; set; }
        public string Anexo { get; set; } = string.Empty;
        public decimal AliquotaNominal { get; set; }
        public decimal AliquotaEfetiva { get; set; }
        public decimal DAS { get; set; }
        public decimal VlrDespFixas { get; set; } = 0;
        public decimal ValorLiquidoMensal { get; set; }
        public DespesasFixasDto DespesasFixas { get; set; } = new();
        public DespesasFixasAnualDto DespesasFixasAnual { get; set; } = new();
        public IEnumerable<SocioResponseDto> Socios { get; set; } = new List<SocioResponseDto>();
        public IEnumerable<FuncionarioResponseDto> Funcionarios { get; set; } = new List<FuncionarioResponseDto>();
        public EncargosDto Encargos { get; set; } = new();
        public EncargosMEIDto EncargosMEI { get; set; } = new();
    }
}
