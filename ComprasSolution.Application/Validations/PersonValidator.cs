using ComprasSolution.Application.DTOs;
using FluentValidation;

namespace ComprasSolution.Application.Validations
{
    public class PersonValidator : AbstractValidator<PersonDTO>
    {
        public PersonValidator() 
        {
            RuleFor(x => x.Document)
                .NotEmpty()
                .NotNull()
                .WithMessage("Documento deve ser informado");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome deve ser informado");

            RuleFor(x => x.Phone)
                .NotEmpty()
                .NotNull()
                .WithMessage("Telefone deve ser informado");
        }
    }
}
