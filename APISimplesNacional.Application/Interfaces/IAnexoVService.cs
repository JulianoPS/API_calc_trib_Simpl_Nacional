using APISimplesNacional.Application.Dtos;

namespace APISimplesNacional.Application.Interfaces
{
    public interface IAnexoVService
    {
        Task<IEnumerable<AnexoBaseDto>> ObterPorEmailOuCelularAsync(string? email, string? celular);
        Task<IEnumerable<AnexoBaseDto>> ObterPorEmpresaIdAsync(int empresaId);
        Task AtualizarAsync(string? email, string? celular, IEnumerable<AnexoVDto> tabelaDto);
    }
}