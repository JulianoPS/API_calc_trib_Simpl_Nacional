using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Infra.Entidades;
using AutoMapper;

namespace APISimplesNacional.Application.Mapping
{
    public class EmpresasProfile : Profile
    {
        public EmpresasProfile()
        {
            // DTO → Entidade
            CreateMap<CriarEmpresaDto, Empresas>();

            // Entidade → DTO
            CreateMap<Empresas, EmpresaResponseDto>()
                .ForMember(dest => dest.Nome, opt => opt.MapFrom(src => src.Nome))
                .ForMember(dest => dest.Celular, opt => opt.MapFrom(src => src.Celular))
                .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
                .ForMember(dest => dest.IrDependente, opt => opt.MapFrom(src => src.IrDependente));
        }
    }
}
