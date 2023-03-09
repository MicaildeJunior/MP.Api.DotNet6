using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interface;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.FiltersDb;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services;

public class PersonService : IPersonService
{
    private readonly IPersonRepository _personRepository;
    private readonly IMapper _mapper;

    public PersonService(IPersonRepository personRepository, IMapper mapper)
    {
        _personRepository = personRepository;
        _mapper = mapper;
    }

    public async Task<ResultService<PersonDTO>> CreateAsync(PersonDTO personDTO)
    {
        if (personDTO == null)
            return ResultService.Fail<PersonDTO>("Objeto deve ser informado");

        var result = new PersonDTOValidator().Validate(personDTO);
        if (!result.IsValid)
            return ResultService.RequestError<PersonDTO>("Problemas de validade!", result);

        // Linha abaixo criamos um novo, diferente da 'Edicao' linha 77, metodo UpdateAsync, pq pra editar precisamos dele mapeado!
        var person = _mapper.Map<Person>(personDTO);
        var data = await _personRepository.CreateAsync(person);
        return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(data));
    }

    public async Task<ResultService> DeleteAsync(int id)
    {
        // Para deletar, primeiro temos que verificar se o id é igual o q esta no banco
        var person = await _personRepository.GetByIdAsync(id);

        if (person == null)
        {
            return ResultService.Fail("Pessoa não encontrada");
        }
        else
        {
            await _personRepository.DeleteAsync(person);

            return ResultService.Ok($"Pessoa do id:{id} e com nome:{person.Name} deletada!");
        }
    }

    public async Task<ResultService<ICollection<PersonDTO>>> GetAsync()
    {
        // Buscamos a Entidade e devolvemos uma Dto
        var people = await _personRepository.GetPeopleAsync();
        return ResultService.Ok<ICollection<PersonDTO>>(_mapper.Map<ICollection<PersonDTO>>(people));
    }

    public async Task<ResultService<PersonDTO>> GetByIdAsync(int id)
    {
        var person = await _personRepository.GetByIdAsync(id);

        if (person == null)
        {
            return ResultService.Fail<PersonDTO>("Pessoa não encontrada!");
        }

        return ResultService.Ok<PersonDTO>(_mapper.Map<PersonDTO>(person));
    }

    public async Task<ResultService<PagedBaseResponseDTO<PersonDTO>>> GetPagedAsync(PersonFilterDb personFilterDb)
    {
        /* Chamou o método no repositório, retornamos dados paginados, 
         * pegamos esses dados da entidade e retornamos em DTO
        */
        var peoplePaged = await _personRepository.GetPagedAsync(personFilterDb);
        var result = new PagedBaseResponseDTO<PersonDTO>(peoplePaged.TotalRegisters,
                                                        _mapper.Map<List<PersonDTO>>(peoplePaged.Data));

        return ResultService.Ok(result);
    }

    public async Task<ResultService> UpdateAsync(PersonDTO personDTO)
    {
        // Caso o id informado não existir
        if (personDTO == null)
        {
            return ResultService.Fail<PersonDTO>("Objeto deve ser informado!");
        }

        // Caso a validação não for válida
        var validation = new PersonDTOValidator().Validate(personDTO);
        if (!validation.IsValid)
        {
            return ResultService.RequestError("Problemas com a validação dos campos", validation);
        }

        // Buscar a Pessoa
        var person = await _personRepository.GetByIdAsync(personDTO.Id);
        if (person == null)
        {
            return ResultService.Fail("Pessoa não encontrada");
        }

        //var person = _mapper.Map<Person>(personDTO); Quando é Inserir

        // Abaixo pegamos a person e mantemos o estado dela, pegamos os dados da DTO e joga pra Entidade person
        // Edição
        person = _mapper.Map<PersonDTO, Person>(personDTO, person);
        await _personRepository.EditAsync(person);
        return ResultService.Ok("Pessoa editada");
    }
}
