using ComprasSolution.Application.DTOs;
using FluentValidation;

namespace ComprasSolution.Application.Validations
{
    public class PurchaseValidation : AbstractValidator<PurchaseDTO>
    {
        public PurchaseValidation()
        {
            RuleFor(x => x.CodErp)
                .NotEmpty()
                .NotNull()
                .WithMessage("CodErp deve ser informado!");

            RuleFor(x => x.Document)
                .NotEmpty()
                .NotNull()
                .WithMessage("Documento deve ser informado");
        }
    }
}
