using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Application.Dtos
{
    public class TabelaIRDto
    {
        public int Faixa { get; set; }
        public decimal LimiteInic { get; set; }
        public decimal LimiteFin { get; set; }
        public decimal Aliquota { get; set; }
        public decimal VlrDeduzir { get; set; }

        public TabelaIRDto() { }

        public TabelaIRDto(TabelaIR entidade)
        {
            
            Faixa = entidade.Faixa;
            LimiteInic = entidade.LimiteInic;
            LimiteFin = entidade.LimiteFin;
            Aliquota = entidade.Aliquota;
            VlrDeduzir = entidade.VlrDeduzir;
        }

        public TabelaIR ToEntity(int empresaId)
        {
            return new TabelaIR
            {
                IdEmpresa = empresaId,
                Faixa = Faixa,
                LimiteInic = LimiteInic,
                LimiteFin = LimiteFin,
                Aliquota = Aliquota,
                VlrDeduzir = VlrDeduzir,
            };
        }
    }

}
