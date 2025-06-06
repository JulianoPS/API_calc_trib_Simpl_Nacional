using APISimplesNacional.Domain.Repositories;
using APISimplesNacional.Infra.Contexto;
using APISimplesNacional.Infra.Entidades;
using Microsoft.EntityFrameworkCore;

namespace APISimplesNacional.Infra.Repositorios
{
    public class AnexoIIIRepositorio : IAnexoIIIRepositorio
    {
        private readonly SimplesNacionalDbContext _context;

        public AnexoIIIRepositorio(SimplesNacionalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnexoIII>> ObterPorEmpresaIdAsync(int empresaId)
        {
            return await _context.AnexoIII
                .AsNoTracking()
                .Where(a => a.IdEmpresa == empresaId)
                .OrderBy(a => a.Faixa)
                .ToListAsync();
        }

        public async Task AtualizarAsync(List<AnexoIII> entidades)
        {
            var empresaId = entidades.First().IdEmpresa;

            var antigas = await _context.AnexoIII
                .Where(a => a.IdEmpresa == empresaId)
                .ToListAsync();
            _context.AnexoIII.RemoveRange(antigas);

            await _context.AnexoIII.AddRangeAsync(entidades);
            await _context.SaveChangesAsync();
        }
    }
}