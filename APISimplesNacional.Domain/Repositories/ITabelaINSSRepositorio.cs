using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Domain.Repositories
{
    public interface ITabelaINSSRepositorio
    {
        Task<IEnumerable<TabelaINSS>> ObterPorEmpresaIdAsync(int empresaId);
        Task AtualizarAsync(List<TabelaINSS> entidades);
    }
}