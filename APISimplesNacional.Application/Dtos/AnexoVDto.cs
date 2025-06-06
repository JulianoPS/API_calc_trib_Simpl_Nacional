using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Application.Dtos
{
    public class AnexoVDto : AnexoBaseDto
    {
        public AnexoVDto() { }

        public AnexoVDto(AnexoV entidade)
        {
            Faixa = entidade.Faixa;
            LimiteInic = entidade.LimiteInic;
            LimiteFin = entidade.LimiteFin;
            Aliquota = entidade.Aliquota;
            VlrDeduzir = entidade.VlrDeduzir;
        }

        public AnexoV ToEntity(int empresaId)
        {
            return new AnexoV
            {
                IdEmpresa = empresaId,
                Faixa = Faixa,
                LimiteInic = LimiteInic,
                LimiteFin = LimiteFin,
                Aliquota = Aliquota,
                VlrDeduzir = VlrDeduzir
            };
        }
    }
}