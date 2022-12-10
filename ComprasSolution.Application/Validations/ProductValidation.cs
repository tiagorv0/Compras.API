using ComprasSolution.Application.DTOs;
using FluentValidation;

namespace ComprasSolution.Application.Validations
{
    public class ProductValidation : AbstractValidator<ProductDTO>
    {
        public ProductValidation()
        {
            RuleFor(x => x.CodErp)
                .NotEmpty()
                .NotNull()
                .WithMessage("CodErp deve ser informado!");

            RuleFor(x => x.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage("Nome deve ser informado!");

            RuleFor(x => x.Price)
                .GreaterThan(0)
                .WithMessage("Preço deve ser maior que zero!");
        }
    }
}
