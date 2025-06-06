using System.ComponentModel.DataAnnotations;

namespace APISimplesNacional.Application.Dtos
{
    public class UpdateIrDependenteDto
    {
        [Required(ErrorMessage = "Valor de desconto por dependente é obrigatório.")]
        public decimal IrDependente { get; set; }
    }
}
