using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Domain.Repositories;

namespace APISimplesNacional.Application.Services
{
    public class ErroLogService : IErroLogService
    {
        private readonly IErroLogRepositorio _erroLogRepositorio;

        public ErroLogService(IErroLogRepositorio erroLogRepositorio)
        {
            _erroLogRepositorio = erroLogRepositorio;
        }

        public async Task RegistrarErroAsync(Exception ex, int statusCode)
        {
            await _erroLogRepositorio.RegistrarErroAsync(ex, statusCode);
        }
    }
}
