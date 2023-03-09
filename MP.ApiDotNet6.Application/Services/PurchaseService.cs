using AutoMapper;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.DTOs.Validations;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Entities;
using MP.ApiDotNet6.Domain.Repositories;
using MP.ApiDotNet6.Infra.Data.Repositories;

namespace MP.ApiDotNet6.Application.Services;

public class PurchaseService : IPurchaseService
{
    private readonly IProductRepository _productRepository;
    private readonly IPersonRepository _personRepository;
    private readonly IPurchaseRepository _purchaseRepository;
    private readonly IMapper _mapper;
    private readonly IUnityOfWork _unityOfWork;

    public PurchaseService(IProductRepository productRepository, IPersonRepository personRepository, IPurchaseRepository purchaseRepository, IMapper mapper, IUnityOfWork unityOfWork)
    {
        _productRepository = productRepository;
        _personRepository = personRepository;
        _purchaseRepository = purchaseRepository;
        _mapper = mapper;
        _unityOfWork = unityOfWork;
    }

    public async Task<ResultService<PurchaseDTO>> CreateAsync(PurchaseDTO purchaseDTO)
    {
        if (purchaseDTO == null)
        {
            return ResultService.Fail<PurchaseDTO>("Objeto deve ser informado!");
        }

        // Validação se os campos obrigatorios foram informados
        var validate = new PurchaseDTOValidator().Validate(purchaseDTO);
        if (!validate.IsValid)
        {
            return ResultService.RequestError<PurchaseDTO>("Problemas de validação", validate);
        }

        try
        {
            await _unityOfWork.BeginTransaction();
            // Buscar produtoId
            var productId = await _productRepository.GetIdByCodeErpAsync(purchaseDTO.CodeErp);
            // Se não achar o produto na base de dados irá criar um produto com um Id novo
            if (productId == 0)
            {
                var product = new Product(purchaseDTO.ProductName, purchaseDTO.CodeErp, purchaseDTO.Price ?? 0);
                await _productRepository.CreateAsync(product);
                productId = product.Id;
            }

            // Buscar PessoaId
            var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);

            // Criar objeto de compra
            var purchase = new Purchase(productId, personId);

            // Inserção do objeto compra no banco
            var data = await _purchaseRepository.CreateAsync(purchase);
            purchaseDTO.Id = data.Id;

            // Se tudo der certo faz o Commit
            await _unityOfWork.Commit();

            return ResultService.Ok<PurchaseDTO>(purchaseDTO);
        }
        catch (Exception ex)
        {
            await _unityOfWork.Rollback();
            return ResultService.Fail<PurchaseDTO>($"{ex.Message}");
        }
    }

    // Método que irá retornar uma lista de compras
    public async Task<ResultService<ICollection<PurchaseDetailDTO>>> GetAsync()
    {
        var purchases = await _purchaseRepository.GetAllAsync();
        return ResultService.Ok(_mapper.Map<ICollection<PurchaseDetailDTO>>(purchases));
    }

    // Método que irá retornar só uma Entidade
    public async Task<ResultService<PurchaseDetailDTO>> GetByIdAsync(int id)
    {
        var purchase = await _purchaseRepository.GetByIdAsync(id);
        if (purchase == null)
        {
            return ResultService.Fail<PurchaseDetailDTO>("Compra não encontrada!");
        }
        return ResultService.Ok(_mapper.Map<PurchaseDetailDTO>(purchase));
    }

    // Método que irá remover uma compra
    public async Task<ResultService> RemoveAsync(int id)
    {
        var purchase = await _purchaseRepository.GetByIdAsync(id);
        if (purchase == null)
        {
            return ResultService.Fail("Compra não encontrada!");
        }
        else
        {
            await _purchaseRepository.DeleteAsync(purchase);
            return ResultService.Ok($"Compra:{id} de deletada!");
        }
    }

    // Método que irá atualizar
    public async Task<ResultService<PurchaseDTO>> UpdateAsync(PurchaseDTO purchaseDTO)
    {
        // Valida se o objeto do controle é nulo
        if (purchaseDTO == null)
        {
            return ResultService.Fail<PurchaseDTO>("Objeto deve ser informado");
        }

        // Caso a validação não for válida
        var validation = new PurchaseDTOValidator().Validate(purchaseDTO);
        if (!validation.IsValid)
        {
            return ResultService.RequestError<PurchaseDTO>("Problemas com a validação", validation);
        }

        // Buscar a Compra na base de dados, caso não estiver la não tem como editar
        var purchase = await _purchaseRepository.GetByIdAsync(purchaseDTO.Id);
        if (purchase == null)
        {
            return ResultService.Fail<PurchaseDTO>("Compra não encontrada na base de dados");
        }

        // Buscou na base de dados quais são os Ids
        var productId = await _productRepository.GetIdByCodeErpAsync(purchaseDTO.CodeErp);
        var personId = await _personRepository.GetIdByDocumentAsync(purchaseDTO.Document);

        /* Método de erro irá ser feito na Entidade Purchase, não no Controller,
         * se pro feito na Controllerteria que dar um 'new' e perderia o rastrio
         * sendo assim é feito na entidade.
        */

        purchase.Edit(purchase.Id, productId, personId);
        await _purchaseRepository.EditAsync(purchase);
        return ResultService.Ok(purchaseDTO);
    }
}
