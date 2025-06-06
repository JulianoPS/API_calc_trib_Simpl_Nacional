namespace APISimplesNacional.Application.Interfaces
{
    public interface IErroLogService
    {
        Task RegistrarErroAsync(Exception ex, int statusCode);
    }
}
