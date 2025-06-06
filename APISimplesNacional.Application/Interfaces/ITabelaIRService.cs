using APISimplesNacional.Application.Dtos;
namespace APISimplesNacional.Application.Interfaces
{
    public interface ITabelaIRService
    {
        Task<IEnumerable<TabelaIRDto>> ObterPorEmailOuCelularAsync(string email, string celular);
        Task<IEnumerable<TabelaIRDto>> ObterPorEmpresaIdAsync(int empresaId);
        Task AtualizarAsync(string? email, string? celular, IEnumerable<TabelaIRDto> tabelaDto);
     
    }
}
