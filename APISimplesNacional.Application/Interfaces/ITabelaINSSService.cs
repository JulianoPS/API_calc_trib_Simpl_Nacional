using APISimplesNacional.Application.Dtos;

namespace APISimplesNacional.Application.Interfaces
{
    public interface ITabelaINSSService
    {
        Task<IEnumerable<TabelaINSSDto>> ObterPorEmailOuCelularAsync(string? email, string? celular);
        Task<IEnumerable<TabelaINSSDto>> ObterPorEmpresaIdAsync(int empresaId);
       
       Task AtualizarAsync(string? email, string? celular, IEnumerable<TabelaINSSDto> tabelaDto);
    }
}