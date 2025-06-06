using APISimplesNacional.Infra.Entidades;
using APISimplesNacional.Application.Dtos;

namespace APISimplesNacional.Application.Interfaces
{
    public interface IEmpresaService
    {
        Task<Empresas> CriarEmpresaComTabelasAsync(CriarEmpresaDto dto);
        Task<Empresas?> ObterPorEmailOuCelularAsync(string? email, string? celular);
        Task<Empresas?> ObterPorIdAsync(int id);
        Task AtualizarIrDependenteAsync(string email, string celular, decimal irDependente);
    }

}
