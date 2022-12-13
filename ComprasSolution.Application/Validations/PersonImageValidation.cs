using ComprasSolution.Application.DTOs;
using FluentValidation;

namespace ComprasSolution.Application.Validations
{
    public class PersonImageValidation : AbstractValidator<PersonImageDTO>
    {
        public PersonImageValidation()
        {
            RuleFor(X => X.PersonId)
                .GreaterThanOrEqualTo(0)
                .WithMessage("PersonId não pode ser menor ou igual a zero!");

            RuleFor(x => x.Image)
                .NotEmpty()
                .NotNull()
                .WithMessage("Imagem deve ser informado");
        }
    }
}
