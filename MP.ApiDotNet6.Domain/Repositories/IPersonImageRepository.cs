using MP.ApiDotNet6.Domain.Entities;

namespace MP.ApiDotNet6.Domain.Repositories;

public interface IPersonImageRepository
{
    // Método que obtem pelo Id
    Task<PersonImage?> GetByIdAsync(int id);

    // Método que cria
    Task<PersonImage> CreateAsync(PersonImage personImage);

    // Método que retorna uma lista 
    Task<ICollection<PersonImage>> GetByPersonIdAsync(int personId);

    // Método que edita
    Task EdityAsync(PersonImage personImage);
}
