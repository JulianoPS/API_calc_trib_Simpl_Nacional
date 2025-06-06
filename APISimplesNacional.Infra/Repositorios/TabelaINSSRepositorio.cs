using APISimplesNacional.Domain.Repositories;
using APISimplesNacional.Infra.Contexto;
using APISimplesNacional.Infra.Entidades;
using Microsoft.EntityFrameworkCore;

namespace APISimplesNacional.Infra.Repositorios
{
    public class TabelaINSSRepositorio : ITabelaINSSRepositorio
    {
        private readonly SimplesNacionalDbContext _context;

        public TabelaINSSRepositorio(SimplesNacionalDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TabelaINSS>> ObterPorEmpresaIdAsync(int empresaId)
        {
            return await _context.TabelaINSS
                .AsNoTracking()
                .Where(t => t.IdEmpresa == empresaId)
                .OrderBy(t => t.Faixa)
                .ToListAsync();
        }

        public async Task AtualizarAsync(List<TabelaINSS> entidades)
        {
            var empresaId = entidades.First().IdEmpresa;

            var antigas = await _context.TabelaINSS
                .Where(t => t.IdEmpresa == empresaId)
                .ToListAsync();
            _context.TabelaINSS.RemoveRange(antigas);

            await _context.TabelaINSS.AddRangeAsync(entidades);
            await _context.SaveChangesAsync();
        }
    }
}