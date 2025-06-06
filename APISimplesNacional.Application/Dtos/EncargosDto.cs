namespace APISimplesNacional.Application.Dtos
{
    /// <summary>
    /// Representa os encargos patronais detalhados para sócios e funcionários.
    /// </summary>
    public class EncargosDto
    {

        public string Msg { get; set; } = "Encargos mais elevados; permite maior flexibilidade e crescimento.";
        /// <summary>
        /// CPP (20%) sobre o valor anual de pró-labore dos sócios, dividido por 12 para obter o valor mensal.
        /// </summary>
        public decimal CppSociosMensal { get; set; }

        /// <summary>
        /// CPP (20%) sobre o valor anual de salários dos funcionários, dividido por 12 para obter o valor mensal.
        /// </summary>
        public decimal CppFuncionariosMensal { get; set; }

        /// <summary>
        /// FGTS (8%) sobre o valor anual de salários dos funcionários, dividido por 12 para obter o valor mensal.
        /// </summary>
        public decimal FgtsMensal { get; set; }

        /// <summary>
        /// Total de encargos: CPP sócios + CPP funcionários + FGTS.
        /// </summary>
        public decimal TotalEncargosMensal { get; set; }
    }
}
