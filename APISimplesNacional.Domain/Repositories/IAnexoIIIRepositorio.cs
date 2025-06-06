using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Domain.Repositories
{
    public interface IAnexoIIIRepositorio
    {
        Task<IEnumerable<AnexoIII>> ObterPorEmpresaIdAsync(int empresaId);
        Task AtualizarAsync(List<AnexoIII> entidades);
    }
}