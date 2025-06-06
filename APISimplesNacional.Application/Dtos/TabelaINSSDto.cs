using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Application.Dtos
{
    public class TabelaINSSDto
    {
        public int Faixa { get; set; }
        public decimal LimiteInic { get; set; }
        public decimal LimiteFin { get; set; }
        public decimal Aliquota { get; set; }
        public decimal Deducao { get; set; }
        public TabelaINSSDto() { }

        public TabelaINSSDto(TabelaINSS entidade)
        {
            Faixa = entidade.Faixa;
            LimiteInic = entidade.LimiteInic;
            LimiteFin = entidade.LimiteFin;
            Aliquota = entidade.Aliquota;
            Deducao = entidade.Deducao;
        }

        public TabelaINSS ToEntity(int empresaId)
        {
            return new TabelaINSS
            {
                IdEmpresa = empresaId,
                Faixa = Faixa,
                LimiteInic = LimiteInic,
                LimiteFin = LimiteFin,
                Aliquota = Aliquota,
                Deducao = Deducao
            };
        }
    }
}