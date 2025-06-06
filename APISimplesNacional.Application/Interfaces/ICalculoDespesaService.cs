using APISimplesNacional.Application.Dtos;

namespace APISimplesNacional.Domain.Interfaces
{
    public interface ICalculoDespesaService
    {
        Task<CalculoResponseDto> CalcularAsync(CalculoRequestDto request);
    }
}