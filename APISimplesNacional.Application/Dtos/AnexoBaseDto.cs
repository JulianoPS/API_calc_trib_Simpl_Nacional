// APISimplesNacional.Application/Dtos/AnexoBaseDto.cs
namespace APISimplesNacional.Application.Dtos
{
    /// <summary>
    /// Abstração de qualquer tabela de anexo (III ou V).
    /// </summary>
    public class AnexoBaseDto
    {
        public int   Faixa     { get; set; }
        public decimal LimiteInic { get; set; }
        public decimal LimiteFin  { get; set; }
        public decimal Aliquota   { get; set; }
        public decimal VlrDeduzir { get; set; }
    }
}
