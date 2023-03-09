using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MP.ApiDotNet6.Api.Authentication;
using MP.ApiDotNet6.Application.DTOs;
using MP.ApiDotNet6.Application.Services;
using MP.ApiDotNet6.Application.Services.Interface;
using MP.ApiDotNet6.Domain.Authentication;
using MP.ApiDotNet6.Domain.FiltersDb;

namespace MP.ApiDotNet6.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : BaseController
    {
        private readonly IPersonService _personService;
        private readonly ICurrentUser _currentUser;
        private List<string> _permissionNeeded = new List<string>(){"admin"};
        private readonly List<string> _permissionUser;

        public PersonController(IPersonService personService, ICurrentUser currentUser)
        {
            _personService = personService;
            _currentUser = currentUser;
            _permissionUser = _currentUser.Permissions?.Split(",")?.ToList() ?? new List<string>();
        }
     
        // Método que vai cadastrar pessoa
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] PersonDTO personDTO)
        {
            _permissionNeeded.Add("CadastraPessoa");
            if(!ValidPermission(_permissionUser,_permissionNeeded))
            {
                return Forbidden();
            }
            var result = await _personService.CreateAsync(personDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }


        // Metodo que ira retornar uma lista de pessoas
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            _permissionNeeded.Add("BuscaPessoa");
            if (!ValidPermission(_permissionUser, _permissionNeeded))
            {
                return Forbidden();
            }
            var result = await _personService.GetAsync();
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // Metodo que ira retornar somente uma pessoa
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            _permissionNeeded.Add("BuscaPessoa");
            if (!ValidPermission(_permissionUser, _permissionNeeded))
            {
                return Forbidden();
            }
            var result = await _personService.GetByIdAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // Metodo de Atualizar
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] PersonDTO personDTO)
        {
            _permissionNeeded.Add("EditaPessoa");
            if (!ValidPermission(_permissionUser, _permissionNeeded))
            {
                return Forbidden();
            }
            var result = await _personService.UpdateAsync(personDTO);
            if (result.IsSuccess)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        // Metodo Deletar pessoa
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            _permissionNeeded.Add("DeletaPessoa");
            if (!ValidPermission(_permissionUser, _permissionNeeded))
            {
                return Forbidden();
            }
            var result = await _personService.DeleteAsync(id);
            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // Método de páginas de query de Pessoas
        // FromQuery é para conseguir concatenando na string a variavel q agnt quer
        [HttpGet]
        [Route("paged")]
        public async Task<ActionResult> GetPagedAsync([FromQuery] PersonFilterDb personFilterDb)
        {
            var result = await _personService.GetPagedAsync(personFilterDb); 
            if (result.IsSuccess)
            {
                return Ok(result);  
            }
            return BadRequest(result);
        }
    }
}
