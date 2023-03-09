using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;

namespace MP.ApiDotNet6.Application.Services;

public class ProductService : IProductService
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    // Construtor
    public ProductService(IProductRepository productRepository, IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    // Método que cria um produto
    public async Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO)
    {
        // Verificação se os objetos são nulos
        if (productDTO == null)
            return ResultService.Fail<ProductDTO>("Objeto deve ser informado");

        // Verificação se os objetos são validos
        var result = new ProductDTOValidator().Validate(productDTO);
        if (!result.IsValid)
            return ResultService.RequestError<ProductDTO>("Problemas na validação!", result);

        // Converter DTO para Produto
        // Quando vai Criar
        var product = _mapper.Map<Product>(productDTO);
        // Pega o produto e insere ele no banco
        var data = await _productRepository.CreateAsync(product);
        // Pega o retorno da nossa inserção e devolve esse retorno
        return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(data));
    }

    // Método que irá trazer uma lista de produtos
    public async Task<ResultService<ICollection<ProductDTO>>> GetAsync()
    {
        // Buscamos a Entidade e devolvemos uma Dto
        var products = await _productRepository.GetProductsAsync();
        return ResultService.Ok<ICollection<ProductDTO>>(_mapper.Map<ICollection<ProductDTO>>(products));
    }

    // Método que irá bucar um produto pelo Id
    public async Task<ResultService<ProductDTO>> GetByIdAsync(int id)
    {
        var product = await _productRepository.GetByIdAsync(id);
        // Valida se produto está vindo nulo
        if (product == null)
        {
            return ResultService.Fail<ProductDTO>("Produto não encontrado!");
        }

        return ResultService.Ok<ProductDTO>(_mapper.Map<ProductDTO>(product));
    }

    // Método que irá Remover um produto
    public async Task<ResultService> RemoveAsync(int id)
    {
        // Para deletar, primeiro temos que verificar se o id é igual o q esta no banco
        var product = await _productRepository.GetByIdAsync(id);

        if (product == null)
        {
            return ResultService.Fail("Produto não encontrado");
        }
        else
        {
            await _productRepository.DeleteAsync(product);
            return ResultService.Ok($"Produto do Id:{id}, e com Nome:{product.Name} deletados!");
        }
    }

    // Método que irá atualizar
    public async Task<ResultService> UpdateAsync(ProductDTO productDTO)
    {
        // Valida se o objeto do controle é nulo
        if (productDTO == null)
        {
            return ResultService.Fail<ProductDTO>("Objeto deve ser informado!");
        }

        // Caso a validação não for válida
        var validation = new ProductDTOValidator().Validate(productDTO);
        if (!validation.IsValid)
        {
            return ResultService.RequestError("Problemas com a validação", validation);
        }

        // Buscar o Produto na base de dados, caso não estiver la não tem como ediar
        var product = await _productRepository.GetByIdAsync(productDTO.Id);
        if (product == null)
        {
            return ResultService.Fail("Produto não encontrado");
        }

        // Abaixo pegamos  product e mantemos o estado dele, pegamos os dados da DTO e joga pra Entidade product 
        // Quando vai Editar sem perder o rastreio, diferente da linha 42 quando vai criar
        product = _mapper.Map<ProductDTO, Product>(productDTO, product);
        await _productRepository.EditAsync(product);
        return ResultService.Ok("Produto editado");
    }
}
