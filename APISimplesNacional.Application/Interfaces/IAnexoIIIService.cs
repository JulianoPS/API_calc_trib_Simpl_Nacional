using APISimplesNacional.Application.Dtos;

namespace APISimplesNacional.Application.Interfaces
{
    public interface IAnexoIIIService
    {
        Task<IEnumerable<AnexoBaseDto>> ObterPorEmailOuCelularAsync(string? email, string? celular);
        Task<IEnumerable<AnexoBaseDto>> ObterPorEmpresaIdAsync(int empresaId);
        Task AtualizarAsync(string? email, string? celular, IEnumerable<AnexoIIIDto> tabelaDto);
    }
}