using System.Text.Json.Serialization;
namespace APISimplesNacional.Infra.Entidades
{
    public class AnexoV
    {
        public int Id { get; set; }
        public int IdEmpresa { get; set; }
        public int Faixa { get; set; }
        public decimal LimiteInic { get; set; }
        public decimal LimiteFin { get; set; }
        public decimal Aliquota { get; set; }
        public decimal VlrDeduzir { get; set; }
        [JsonIgnore] public Empresas Empresa { get; set; } = null!;
    }
}
