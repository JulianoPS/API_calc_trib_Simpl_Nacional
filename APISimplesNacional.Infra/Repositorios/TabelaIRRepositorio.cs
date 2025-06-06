using APISimplesNacional.Domain.Repositories;
using APISimplesNacional.Infra.Contexto;
using APISimplesNacional.Infra.Entidades;
using Microsoft.EntityFrameworkCore;

namespace APISimplesNacional.Infra.Repositorios
{
    public class TabelaIRRepositorio : ITabelaIRRepositorio
    {
        private readonly SimplesNacionalDbContext _context;
        public TabelaIRRepositorio(SimplesNacionalDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<TabelaIR>> ObterPorEmpresaIdAsync(int empresaId)
        {
            return await _context.TabelaIR
                .AsNoTracking()
                .Where(t => t.IdEmpresa == empresaId)
                .OrderBy(t => t.Faixa)
                .ToListAsync();
        }
        public async Task AtualizarAsync(List<TabelaIR> entidades)
        {
            var empresaId = entidades.First().IdEmpresa;

            // Remove antigas
            var antigas = await _context.TabelaIR.Where(t => t.IdEmpresa == empresaId).ToListAsync();
            _context.TabelaIR.RemoveRange(antigas);

            // Adiciona novas
            await _context.TabelaIR.AddRangeAsync(entidades);
            await _context.SaveChangesAsync();
        }
    }
}
