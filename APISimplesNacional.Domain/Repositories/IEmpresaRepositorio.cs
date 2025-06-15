using APISimplesNacional.Infra.Entidades;
public interface IEmpresaRepositorio
{
    Task<Empresas?> ObterPorIdAsync(int id);
    Task<Empresas?> ObterPorEmailOuCelularAsync(string? email, string? celular);
    Task AdicionarAsync(Empresas empresa);
    Task AtualizarAsync(Empresas empresa);
}
