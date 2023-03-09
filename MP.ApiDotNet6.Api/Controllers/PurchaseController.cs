using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.Services;
using MP.ApiDotNet6.Application.Services.Interfaces;
using MP.ApiDotNet6.Domain.Validations;

namespace MP.ApiDotNet6.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class PurchaseController : ControllerBase
{
    private readonly IPurchaseService _purchaseService;

    public PurchaseController(IPurchaseService purchaseService)
    {
        _purchaseService = purchaseService;
    }

    [HttpPost]
    public async Task<ActionResult> PostAsync([FromBody] PurchaseDTO purchaseDTO)
    {
        try
        {
            var result = await _purchaseService.CreateAsync(purchaseDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        catch (DomainValidationException ex)
        {
            var result = ResultService.Fail(ex.Message);
            return BadRequest(result);
        }
    }

    [HttpGet]
    public async Task<ActionResult> GetAsync()
    {
        var result = await _purchaseService.GetAsync();
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    [HttpGet]
    [Route("{id}")]
    public async Task<ActionResult> GetByIdAsync(int id)
    {
        var result = await _purchaseService.GetByIdAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        }
        return BadRequest(result);
    }

    // Método para Editar
    [HttpPut]
    public async Task<ActionResult> EditAsync([FromBody] PurchaseDTO purchaseDTO)
    {
        try
        {
            var result = await _purchaseService.UpdateAsync(purchaseDTO); 
            if(result.IsSuccess)
            {
                return Ok(result);
            }
            return BadRequest(result);
        }
        catch (DomainValidationException ex)
        {
            var result = ResultService.Fail(ex.Message);
            return BadRequest(result);
        }
    }

    // Método para remover
    [HttpDelete]
    [Route("{id}")]
    public async Task<ActionResult> RemoveAsync(int id)
    {
        var result = await _purchaseService.RemoveAsync(id);
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}