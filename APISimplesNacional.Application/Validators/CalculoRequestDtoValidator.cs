using APISimplesNacional.Application.Dtos;
using FluentValidation;

namespace APISimplesNacional.Application.Validators
{
    public class CalculoRequestDtoValidator : AbstractValidator<CalculoRequestDto>
    {
        public CalculoRequestDtoValidator()
        {
            RuleFor(x => x.Atividade)
                .NotEmpty().WithMessage("A atividade é obrigatória.");

            RuleFor(x => x.FaturamentoMensal)
                .GreaterThan(0).WithMessage("Faturamento mensal é obrigatório e deve ser maior que zero.");

            RuleFor(x => x.Socios)
                .NotNull().WithMessage("A lista de sócios não pode ser nula.");

            RuleFor(x => x.Funcionarios)
                .NotNull().WithMessage("A lista de funcionários não pode ser nula.");

            RuleFor(x => x.DespesasFixas)
                .NotNull().WithMessage("O bloco de despesas fixas é obrigatório.");

            When(x => x.DespesasFixas != null, () =>
            {
                RuleFor(x => x.DespesasFixas.Contador)
                    .GreaterThanOrEqualTo(0).WithMessage("Valor do contador deve ser maior ou igual a zero.");

                RuleFor(x => x.DespesasFixas.AluguelSala)
                    .GreaterThanOrEqualTo(0).WithMessage("Aluguel da sala deve ser maior ou igual a zero.");

                RuleFor(x => x.DespesasFixas.Internet)
                    .GreaterThanOrEqualTo(0).WithMessage("Valor da internet deve ser maior ou igual a zero.");

                RuleFor(x => x.DespesasFixas.AguaEenergia)
                    .GreaterThanOrEqualTo(0).WithMessage("Valor de água e energia deve ser maior ou igual a zero.");
            });
        }
    }
}
