using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Domain.Repositories
{
    public interface IAnexoVRepositorio
    {
        Task<IEnumerable<AnexoV>> ObterPorEmpresaIdAsync(int empresaId);
        Task AtualizarAsync(List<AnexoV> entidades);
    }
}