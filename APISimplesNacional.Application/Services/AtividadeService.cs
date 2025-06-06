using APISimplesNacional.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APISimplesNacional.Application.Services
{
    public class AtividadeService : IAtividadeService
    {
        // Lista única, mantida em um campo estático.
        private static readonly List<string> _listaAtividades = new()
        {
            "Administração e locação de imóveis de terceiros",
            "Academia de dança, capoeira, ioga, lutas e artes marciais",
            "Academia ou escola de atividades físicas e esportes",
            "TI e desenvolvedor de programas de computadores e sites",
            "Laboratório de análises clínicas ou de patologia clínica",
            "Serviços de tomografia, diagnósticos médicos por imagem, ressonância magnética, registros gráficos e métodos óticos",
            "Serviços de prótese em geral",
            "Fisioterapia",
            "Medicina, inclusive laboratorial, e enfermagem",
            "Medicina veterinária",
            "Odontologia e prótese dentária",
            "Psicologia, psicanálise, terapia ocupacional, acupuntura, podologia, fonoaudiologia, nutrição, vacinação e bancos de leite",
            "Serviços de comissaria, de despachantes, de tradução e de interpretação",
            "Arquitetura e urbanismo",
            "Engenharia, medição, cartografia, topografia, geologia, geodésia, testes, suporte e análises técnicas e tecnológicas, pesquisa, design, desenho e agronomia",
            "Representação comercial e demais atividades de intermediação de negócios e serviços de terceiros",
            "Perícia, leilão e avaliação",
            "Auditoria, economia, consultoria, gestão, organização, controle e administração",
            "Jornalismo e publicidade",
            "Agenciamento",
            "Outros serviços de natureza intelectual, técnica, científica, desportiva, artística ou cultural",
            "Minha atividade não está na lista"
        };

        public Task<IEnumerable<string>> ObterTodasAsync()
        {
            // Retorna uma cópia da lista (IEnumerable) para evitar modificações externas
            return Task.FromResult(_listaAtividades.AsEnumerable());
        }

        public Task<bool> AtividadeValidaAsync(string atividade)
        {
            if (string.IsNullOrWhiteSpace(atividade))
                return Task.FromResult(false);

            // Verifica ignorando caixa (OrdinalIgnoreCase)
            bool existe = _listaAtividades
                .Any(a => a.Equals(atividade.Trim(), StringComparison.OrdinalIgnoreCase));

            return Task.FromResult(existe);
        }
    }
}
