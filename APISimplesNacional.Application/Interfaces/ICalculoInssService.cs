using APISimplesNacional.Application.Dtos;

namespace APISimplesNacional.Application.Interfaces
{
    public interface ICalculoInssService
    {
        Task<IEnumerable<FuncionarioResponseDto>> CalcularInssFuncionariosAsync(IEnumerable<FuncionarioDto> funcionarios, string? email, string? celular);
        Task<IEnumerable<SocioResponseDto>> CalcularInssSociosAsync(IEnumerable<SocioDto> socios, string? email, string? celular);
    }
}
