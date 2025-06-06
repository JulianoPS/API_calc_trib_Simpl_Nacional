using System.ComponentModel.DataAnnotations;

namespace APISimplesNacional.Application.Dtos
{
    /// <summary>
    /// DTO para criação de uma nova empresa.
    /// Nome, celular e email são obrigatórios para futura identificação.
    /// </summary>
    public class CriarEmpresaDto
    {
        [Required(ErrorMessage = "O nome da empresa é obrigatório.")]
        public required string Nome { get; set; }

        [Required(ErrorMessage = "O celular é obrigatório.")]
        [Phone(ErrorMessage = "Número de celular inválido.")]
        public required string Celular { get; set; }

        [Required(ErrorMessage = "O email é obrigatório.")]
        [EmailAddress(ErrorMessage = "Email inválido.")]
        public required string Email { get; set; }

        [Required(ErrorMessage = "O desconto por dependente para IR é obrigatório.")]
        [Range(0, double.MaxValue, ErrorMessage = "O desconto por dependente deve ser um valor positivo.")]
        public required decimal IrDependente { get; set; }

    }
}
