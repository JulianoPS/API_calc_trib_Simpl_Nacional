using APISimplesNacional.Application.Dtos;

using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Domain.Repositories;

namespace APISimplesNacional.Application.Services
{
    public class TabelaIRService : ITabelaIRService
    {
        private readonly IEmpresaService _empresaService;
        private readonly ITabelaIRRepositorio _tabelaRepositorio;

        public TabelaIRService(IEmpresaService empresaService, ITabelaIRRepositorio tabelaRepositorio)
        {
            _empresaService = empresaService;
            _tabelaRepositorio = tabelaRepositorio;
        }

        public async Task<IEnumerable<TabelaIRDto>> ObterPorEmailOuCelularAsync(string? email, string? celular)
        {
            var empresa = await _empresaService.ObterPorEmailOuCelularAsync(email, celular)
                           ?? await _empresaService.ObterPorIdAsync(1);
            if (empresa == null)
                throw new ArgumentNullException(nameof(empresa));
            var entidades = await _tabelaRepositorio.ObterPorEmpresaIdAsync(empresa.Id);
            return entidades.Select(t => new TabelaIRDto(t));
        }

        public async Task AtualizarAsync(string? email, string? celular, IEnumerable<TabelaIRDto> dto)
        {
            var empresa = await _empresaService.ObterPorEmailOuCelularAsync(email, celular);
            if (empresa == null)
                throw new InvalidOperationException("Empresa não encontrada. Cadastre a empresa primeiro.");

            if (empresa.Id == 1)
                throw new InvalidOperationException("Não é permitido alterar os dados da empresa padrão.");

            var entidades = dto.Select(d => d.ToEntity(empresa.Id)).ToList();
            await _tabelaRepositorio.AtualizarAsync(entidades);
        }
        public async Task<IEnumerable<TabelaIRDto>> ObterPorEmpresaIdAsync(int empresaId)
        {
            var entidades = await _tabelaRepositorio.ObterPorEmpresaIdAsync(empresaId);
            return entidades.Select(t => new TabelaIRDto(t));
        }
    }
}
