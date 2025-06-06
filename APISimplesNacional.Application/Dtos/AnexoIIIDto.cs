using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Application.Dtos
{
    public class AnexoIIIDto : AnexoBaseDto
    {
        
        public AnexoIIIDto() { }

        public AnexoIIIDto(AnexoIII entidade)
        {
            Faixa = entidade.Faixa;
            LimiteInic = entidade.LimiteInic;
            LimiteFin = entidade.LimiteFin;
            Aliquota = entidade.Aliquota;
            VlrDeduzir = entidade.VlrDeduzir;
        }

        public AnexoIII ToEntity(int empresaId)
        {
            return new AnexoIII
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