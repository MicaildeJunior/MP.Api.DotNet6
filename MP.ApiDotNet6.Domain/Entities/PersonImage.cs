using MP.ApiDotNet6.Domain.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Domain.Entities;

public class PersonImage
{
    public PersonImage(int personId, string? imageUri, string imageBase)
    {
        Validation(personId);
        ImageUri = imageUri;
        ImageBase = imageBase;
    }

    public int Id { get; private set; }
    public int PersonId { get; private set; }
    public string? ImageUri { get; private set; }
    public string? ImageBase { get; private set; } // Não estava recebendo valor nulo,
    public Person Person { get; set; }

    private void Validation(int personId)
    {
        DomainValidationException.When(personId == 0, "Id deve ser informado");
        PersonId = personId;
    }
}
