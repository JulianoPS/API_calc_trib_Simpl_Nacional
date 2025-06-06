using APISimplesNacional.Infra.Contexto;
using APISimplesNacional.Infra.Entidades;
using Microsoft.EntityFrameworkCore;

public class ClonagemRepositorio : IClonagemRepositorio
{
    private readonly SimplesNacionalDbContext _context;

    public ClonagemRepositorio(SimplesNacionalDbContext context)
    {
        _context = context;
    }

    public async Task ClonarTabelasBaseParaEmpresa(int novaEmpresaId, int baseEmpresaId)
    {
        await ClonarTabelaAsync<AnexoIII>(x => new AnexoIII
        {
            IdEmpresa = novaEmpresaId,
            Faixa = x.Faixa,
            LimiteInic = x.LimiteInic,
            LimiteFin = x.LimiteFin,
            Aliquota = x.Aliquota,
            VlrDeduzir = x.VlrDeduzir
        }, x => x.IdEmpresa == baseEmpresaId);

        await ClonarTabelaAsync<AnexoV>(x => new AnexoV
        {
            IdEmpresa = novaEmpresaId,
            Faixa = x.Faixa,
            LimiteInic = x.LimiteInic,
            LimiteFin = x.LimiteFin,
            Aliquota = x.Aliquota,
            VlrDeduzir = x.VlrDeduzir
        }, x => x.IdEmpresa == baseEmpresaId);

        await ClonarTabelaAsync<TabelaINSS>(x => new TabelaINSS
        {
            IdEmpresa = novaEmpresaId,
            Faixa = x.Faixa,
            LimiteInic = x.LimiteInic,
            LimiteFin = x.LimiteFin,
            Aliquota = x.Aliquota,
            Deducao = x.Deducao
        }, x => x.IdEmpresa == baseEmpresaId);

        await ClonarTabelaAsync<TabelaIR>(x => new TabelaIR
        {
            IdEmpresa = novaEmpresaId,
            Faixa = x.Faixa,
            LimiteInic = x.LimiteInic,
            LimiteFin = x.LimiteFin,
            Aliquota = x.Aliquota,
            VlrDeduzir = x.VlrDeduzir
        }, x => x.IdEmpresa == baseEmpresaId);

        await _context.SaveChangesAsync();
    }

    private async Task ClonarTabelaAsync<T>(Func<T, T> map, Func<T, bool> filtro) where T : class
    {
        var listaBase = await _context.Set<T>().AsNoTracking().ToListAsync();
        var clones = listaBase.Where(filtro).Select(map).ToList();
        _context.Set<T>().AddRange(clones);
    }
}
