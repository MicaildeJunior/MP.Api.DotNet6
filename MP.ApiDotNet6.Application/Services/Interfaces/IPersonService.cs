using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Domain.FiltersDb;

namespace MP.ApiDotNet6.Application.Services.Interface;

public interface IPersonService
{
    // Vai receber um personDTO, vai gravar nessa pessoa e vai devolver um personDTO
    Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO);

    // Metodo para listar todas as pessoas
    Task<ResultService<ICollection<PersonDTO>>> GetAsync();

    // Metodo que vai listar somente uma pessoa
    Task<ResultService<PersonDTO>> GetByIdAsync(int id);

    // Metodo que vai Cadastrar uma pessoa
    Task<ResultService> UpdateAsync(PersonDTO personDTO);

    // Metodo que vai Deletar uma pessoa
    Task<ResultService> DeleteAsync(int id);
    Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilterDb);
}
