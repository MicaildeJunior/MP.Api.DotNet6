using MP.ApiDotNet6.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MP.ApiDotNet6.Application.Services.Interfaces;

public interface IProductService
{
    // Vai receber uma DTO vai transformar em uma Entidade salvar e retornar uma DTO
    Task<ResultService<ProductDTO>> CreateAsync(ProductDTO productDTO);
    
    // Método que vai trazer a lista de produtos
    Task<ResultService<ICollection<ProductDTO>>> GetAsync();
    
    // Metodo que vai listar somente um produto
    Task<ResultService<ProductDTO>> GetByIdAsync(int id);

    // Método que vai cadastrar um produto
    Task<ResultService> UpdateAsync(ProductDTO productDTO);

    // Método que vai remover um produto
    Task<ResultService> RemoveAsync(int id);
}
