using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Domain.Repositories;

namespace APISimplesNacional.Application.Services
{
    public class AnexoVService : IAnexoVService
    {
        private readonly IEmpresaService _empresaService;
        private readonly IAnexoVRepositorio _repositorio;

        public AnexoVService(
            IEmpresaService empresaService,
            IAnexoVRepositorio repositorio)
        {
            _empresaService = empresaService;
            _repositorio = repositorio;
        }

        public async Task<IEnumerable<AnexoBaseDto>> ObterPorEmailOuCelularAsync(
            string? email, string? celular)
        {
            var empresa = await _empresaService
                .ObterPorEmailOuCelularAsync(email, celular)
                ?? await _empresaService.ObterPorIdAsync(1);
            if (empresa == null) throw new ArgumentNullException(nameof(empresa));

            var entidades = await _repositorio.ObterPorEmpresaIdAsync(empresa.Id);
            return entidades.Select(a => new AnexoVDto(a));
        }

        public async Task<IEnumerable<AnexoBaseDto>> ObterPorEmpresaIdAsync(int empresaId)
        {
            var entidades = await _repositorio.ObterPorEmpresaIdAsync(empresaId);
            return entidades.Select(a => new AnexoVDto(a));
        }

        public async Task AtualizarAsync(
            string? email, string? celular,
            IEnumerable<AnexoVDto> tabelaDto)
        {
            var empresa = await _empresaService
                .ObterPorEmailOuCelularAsync(email, celular);
            if (empresa == null)
                throw new InvalidOperationException("Empresa não encontrada. Cadastre a empresa primeiro.");

            if (empresa.Id == 1)
                throw new InvalidOperationException("Não é permitido alterar os dados da empresa padrão.");

            var entidades = tabelaDto.Select(d => d.ToEntity(empresa.Id)).ToList();
            await _repositorio.AtualizarAsync(entidades);
        }
    }
}