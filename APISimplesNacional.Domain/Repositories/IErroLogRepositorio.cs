namespace APISimplesNacional.Domain.Repositories
{
    public interface IErroLogRepositorio
    {
        Task RegistrarErroAsync(Exception ex, int statusCode);
    }
}
