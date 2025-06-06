
using APISimplesNacional.Infra.Contexto;
using APISimplesNacional.Infra.Entidades;
using Microsoft.EntityFrameworkCore;

public class EmpresaRepositorio : IEmpresaRepositorio
{
    private readonly SimplesNacionalDbContext _context;

    public EmpresaRepositorio(SimplesNacionalDbContext context)
    {
        _context = context;
    }

//    public async Task<IEnumerable<Empresas>> IEmpresaRepositorio.ObterTodasAsync()
//    => await _context.Empresas.ToListAsync();
    

    public async Task<Empresas> ObterPorIdAsync(int id)
        => await _context.Empresas.FindAsync(id);

    public async Task AdicionarAsync(Empresas empresa)
    {
        _context.Empresas.Add(empresa);
        await _context.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Empresas empresa)
    {
        _context.Empresas.Update(empresa);
        await _context.SaveChangesAsync();
    }

    public async Task RemoverAsync(Empresas empresa)
    {
        _context.Empresas.Remove(empresa);
        await _context.SaveChangesAsync();
    }

    public async Task<Empresas?> ObterPorEmailOuCelularAsync(string? email, string? celular)
    {
        if (string.IsNullOrWhiteSpace(email) && string.IsNullOrWhiteSpace(celular))
            return await ObterPorIdAsync(1);

        var empresa = await _context.Empresas
            .AsNoTracking()
            .FirstOrDefaultAsync(e =>
                (!string.IsNullOrWhiteSpace(email) && e.Email == email) ||
                (!string.IsNullOrWhiteSpace(celular) && e.Celular == celular)
            );

        return empresa ?? await ObterPorIdAsync(1);
    }



}
