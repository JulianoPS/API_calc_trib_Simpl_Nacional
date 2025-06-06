using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Domain.Repositories
{
    public interface ITabelaIRRepositorio
    {
        Task<IEnumerable<TabelaIR>> ObterPorEmpresaIdAsync(int empresaId);
        Task AtualizarAsync(List<TabelaIR> entidades);
    }
}
