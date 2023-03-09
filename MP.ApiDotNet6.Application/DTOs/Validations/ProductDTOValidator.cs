using FluentValidation;

namespace MP.ApiDotNet6.Application.DTOs.Validations;

public class ProductDTOValidator : AbstractValidator<ProductDTO>
{
    public ProductDTOValidator()
    {
        //RuleFor(x => x.Id)
        //    .NotEmpty()
        //    .NotNull()
        //    .WithMessage("Id deve ser informado!");

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name deve ser informado!");

        RuleFor(x => x.CodeErp)
            .NotEmpty()
            .NotNull()
            .WithMessage("CodeErp deve ser informado!");

        RuleFor(x => x.Price)
            .GreaterThan(0)
            .WithMessage("Price deve ser maior que zero");
    }
}
