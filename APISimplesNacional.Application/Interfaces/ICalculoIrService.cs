using APISimplesNacional.Application.Dtos;

namespace APISimplesNacional.Application.Interfaces
{
    public interface ICalculoIrService
    {
        Task<IEnumerable<FuncionarioResponseDto>> CalcularIrFuncionariosAsync(IEnumerable<FuncionarioDto> funcionarios, string? email, string? celular);
        Task<IEnumerable<SocioResponseDto>> CalcularIrSociosAsync(IEnumerable<SocioDto> socios, string? email, string? celular);
    }
}
