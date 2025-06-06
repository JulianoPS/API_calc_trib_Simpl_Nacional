namespace APISimplesNacional.Infra.Entidades
{
    public class Empresas
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Celular { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public decimal IrDependente { get; set; } = 0m;
        public decimal IrVlrIsento { get; set; } = 0m;

        public ICollection<TabelaIR> TabelaIRs { get; set; } = new List<TabelaIR>();
        public ICollection<TabelaINSS> TabelaINSSs { get; set; } = new List<TabelaINSS>();
        public ICollection<AnexoIII> AnexosIII { get; set; } = new List<AnexoIII>();
        public ICollection<AnexoV> AnexosV { get; set; } = new List<AnexoV>();
    }
}
