using APISimplesNacional.Domain.Repositories;
using APISimplesNacional.Infra.Contexto;
using APISimplesNacional.Infra.Entidades;
using Microsoft.EntityFrameworkCore;

namespace APISimplesNacional.Infra.Repositorios
{
    public class AnexoVRepositorio : IAnexoVRepositorio
    {
        private readonly SimplesNacionalDbContext _context;

        public AnexoVRepositorio(SimplesNacionalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<AnexoV>> ObterPorEmpresaIdAsync(int empresaId)
        {
            return await _context.AnexoV
                .AsNoTracking()
                .Where(a => a.IdEmpresa == empresaId)
                .OrderBy(a => a.Faixa)
                .ToListAsync();
        }

        public async Task AtualizarAsync(List<AnexoV> entidades)
        {
            var empresaId = entidades.First().IdEmpresa;

            var antigas = await _context.AnexoV
                .Where(a => a.IdEmpresa == empresaId)
                .ToListAsync();
            _context.AnexoV.RemoveRange(antigas);

            await _context.AnexoV.AddRangeAsync(entidades);
            await _context.SaveChangesAsync();
        }
    }
}