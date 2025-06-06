using APISimplesNacional.Application.Dtos;
using APISimplesNacional.Application.Interfaces;
using APISimplesNacional.Infra.Entidades;

namespace APISimplesNacional.Application.Services
{
    public class EmpresaService : IEmpresaService
    {
        private readonly IEmpresaRepositorio _empresaRepositorio;
        private readonly IClonagemRepositorio _clonagemRepositorio;

        public EmpresaService(
            IEmpresaRepositorio empresaRepositorio,
            IClonagemRepositorio clonagemRepositorio)
        {
            _empresaRepositorio = empresaRepositorio;
            _clonagemRepositorio = clonagemRepositorio;
        }

        public async Task<Empresas?> ObterPorEmailOuCelularAsync(string? email, string? celular)
        {
            return await _empresaRepositorio.ObterPorEmailOuCelularAsync(email, celular);
        }

        public async Task<Empresas?> ObterPorIdAsync(int id)
        {
            return await _empresaRepositorio.ObterPorIdAsync(id);
        }


        public async Task<Empresas> CriarEmpresaComTabelasAsync(CriarEmpresaDto dto)
        {
            var existente = await _empresaRepositorio.ObterPorEmailOuCelularAsync(dto.Email, dto.Celular);
            if ((existente != null) && (!existente.Celular.Equals("(62)99213-7872")))
                throw new InvalidOperationException("Já existe uma empresa com este e-mail ou celular.");

            var novaEmpresa = new Empresas
            {
                Nome = dto.Nome,
                Email = dto.Email,
                Celular = dto.Celular
            };

            await _empresaRepositorio.AdicionarAsync(novaEmpresa);

            var novaEmpresaId = novaEmpresa.Id;
            var baseEmpresaId = 1;

            // Clonagem via serviço da camada de infra
            await _clonagemRepositorio.ClonarTabelasBaseParaEmpresa(novaEmpresaId, baseEmpresaId);

            return novaEmpresa;
        }

        public async Task AtualizarIrDependenteAsync(string? email, string? celular, decimal irDependente)
        {
            var empresa = await ObterPorEmailOuCelularAsync(email, celular)
                      ?? await ObterPorIdAsync(1);

            if (empresa == null)
                throw new InvalidOperationException("Empresa não encontrada.");

            if (empresa.Id == 1)
                throw new InvalidOperationException("Não é permitido alterar empresa padrão.");

            // Atualiza campo e persiste
            empresa.IrDependente = irDependente;
            await _empresaRepositorio.AtualizarAsync(empresa);
        }
    }
}
