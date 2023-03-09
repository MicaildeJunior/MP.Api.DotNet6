using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.Services.Interfaces;
using System.Security.Cryptography.X509Certificates;

namespace MP.ApiDotNet6.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] ProductDTO productDTO)
    {
        var result = await _productService.CreateAsync(productDTO);
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    // Metodo que ira retornar uma lista de produtos
    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        var result = await _productService.GetAsync();
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    // Metodo que ira retornar somente um produto pelo id
    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var result = await _productService.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    // Método de atualizar
    [HttpPut]
    public async Task<ActionResult> UpdateAsyncnc([FromBody] ProductDTO productDTO)
    {
        var result = await _productService.UpdateAsync(productDTO);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        else
        {
            return BadRequest(result);
        }      
    }

    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> DeleteAsync(int id)
    {
        var result = await _productService.RemoveAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
