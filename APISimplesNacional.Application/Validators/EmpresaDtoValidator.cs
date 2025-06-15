using FluentValidation;
using APISimplesNacional.Application.Dtos;

namespace APISimplesNacional.Application.Validators
{
    public class EmpresaDtoValidator : AbstractValidator<CriarEmpresaDto>
    {
        public EmpresaDtoValidator()
        {
            RuleFor(x => x.Nome)
                .NotEmpty().WithMessage("O nome da empresa é obrigatório.");

            RuleFor(x => x.Celular)
                .NotEmpty().WithMessage("O celular é obrigatório.")
                .Matches(@"^\+?\d{8,}$").WithMessage("Número de celular inválido.");
            // ajuste a regex conforme sua máscara

            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("O email é obrigatório.")
                .EmailAddress().WithMessage("Email inválido.");

            RuleFor(x => x.IrDependente)
                .GreaterThanOrEqualTo(0).WithMessage("O desconto por dependente deve ser um valor positivo.");
        }
    }
}
