using System.Collections.Generic;
using System.Threading.Tasks;

namespace APISimplesNacional.Application.Interfaces
{
    /// <summary>
    /// Serviço para fornecer e validar a lista de atividades permitidas no Simples Nacional.
    /// </summary>
    public interface IAtividadeService
    {
        /// <summary>
        /// Retorna todas as atividades válidas para Simples Nacional.
        /// </summary>
        Task<IEnumerable<string>> ObterTodasAsync();

        /// <summary>
        /// Verifica se a atividade informada está na lista de atividades válidas (ignora caixa).
        /// </summary>
        Task<bool> AtividadeValidaAsync(string atividade);
    }
}
