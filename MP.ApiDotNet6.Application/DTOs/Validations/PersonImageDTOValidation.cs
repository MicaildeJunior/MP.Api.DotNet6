using FluentValidation;

namespace MP.ApiDotNet6.Application.DTOs.Validations;

public class PersonImageDTOValidation : AbstractValidator<PersonImageDTO>
{
    public PersonImageDTOValidation()
    {
        RuleFor(p => p.PersonId)
            .GreaterThanOrEqualTo(0) // Se ele for maior ou igual
            .WithMessage("PersonId não pode ser menor ou igual a zero")
            .NotEmpty()
            .NotNull();

        RuleFor(p => p.Image)
            .NotNull()
            .NotEmpty()
            .WithMessage("Image deve ser informada");
    }
}
