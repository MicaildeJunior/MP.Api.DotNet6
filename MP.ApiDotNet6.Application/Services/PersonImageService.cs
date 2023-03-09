using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Integrations;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services;

public class PersonImageService : IPersonImageService
{
    private readonly IPersonImageRepository _personImageRepository;
    private readonly IPersonRepository _personRepository;
    private readonly ISavePersonImage _savePersonImage;

    public PersonImageService(IPersonImageRepository personImageRepository, IPersonRepository personRepository, ISavePersonImage savePersonImage)
    {
        _personImageRepository = personImageRepository;
        _personRepository = personRepository;
        _savePersonImage = savePersonImage;
    }

    public async Task<ResultService> CreateImageAsync(PersonImageDTO personImageDTO)
    {
        // Verificaçao se o objeto é nulo
        if (personImageDTO == null)
        {
            return ResultService.Fail("Objeto deve ser informado");
        }

        // Validando os dados que estoa vindo
        var validations = new PersonImageDTOValidation().Validate(personImageDTO);
        if (!validations.IsValid)
        {
            return ResultService.RequestError("Problemas de validação", validations);
        }

        // Pega o repositório da Image Pessoa, obtem pelo Id, chamando o parâmetro da DTO pelo id da pessoa
        var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
        // Validação se existe
        if (person == null)
        {
            return ResultService.Fail("Pessoa não encontrado"); ;
        }

        // Salvando no repositorio
        var pathImage = _savePersonImage.Save(personImageDTO.Image);
        
        // Instancia da PersonImage trazendo os dados do construtor
        var personImage = new PersonImage(person.Id, pathImage,null);
        
        // Salvando no banco
        await _personImageRepository.CreateAsync(personImage);
        return ResultService.Ok("Imagem salva"); 
    }

    public async Task<ResultService> CreateImageBase64Async(PersonImageDTO personImageDTO)
    {
        if (personImageDTO == null)
        { 
            return ResultService.Fail("Objeto deve ser informado");
        }

        var validations = new PersonImageDTOValidation().Validate(personImageDTO);
        if (!validations.IsValid)
        {
            return ResultService.RequestError("Problemas de validação", validations);
        }

        // Pega o repositório da Image Pessoa, obtem pelo Id, chamando o parâmetro da DTO pelo id da pessoa
        var person = await _personRepository.GetByIdAsync(personImageDTO.PersonId);
        // Validação se existe
        if (person == null)
        {
            return ResultService.Fail("Id pessoa não encontrado"); ;
        }

        // Ela existindo, instancia a personImage e tras as informções, emd seguida no await salva ele no banco
        var personImage = new PersonImage(person.Id,null,personImageDTO.Image);
        await _personImageRepository.CreateAsync(personImage);
        return ResultService.Ok("Imagem em base64 salva");

        
    }
}
