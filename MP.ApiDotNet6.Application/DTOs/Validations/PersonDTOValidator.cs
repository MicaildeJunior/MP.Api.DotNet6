using FluentValidation;
using MP.ApiDotNet6.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Application.DTOs.Validations;

public class PersonDTOValidator : AbstractValidator<PersonDTO>
{
    public PersonDTOValidator()
    {
        RuleFor(x => x.Document)
            .NotEmpty()
            .NotNull()
            .WithMessage("Document deve ser informado!");

        RuleFor(x => x.Name)
            .NotEmpty()
            .NotNull()
            .WithMessage("Name deve ser informado!");

        RuleFor(x => x.Phone)
            .NotEmpty()
            .NotNull()
            .WithMessage("Phone deve ser informado!");
    }
}
