using CadastroMembrosApi.Models;
using CadastroMembrosApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CadastroMembrosApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class MembrosController : ControllerBase
{
    private IMembroService _membroService;

    public MembrosController(IMembroService membroService)
    {
        _membroService = membroService;
    }

    [HttpGet]
    public async Task<ActionResult<IAsyncEnumerable<Membro>>> GetMembros()
    {
        try
        {

            var membros = await _membroService.GetMembros();
            if(membros.Count() == 0)
            {
                return NotFound("Lista de membros vazia.");
            }

            return Ok(membros);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter membros");
        }
    }

    [HttpGet("AlunosPorNome")]
    public async Task<ActionResult<IAsyncEnumerable<Membro>>> GetMembrosByName([FromQuery] string name)
    {
        try
        {
            var membros = await _membroService.GetMembroByName(name);

            if (membros.Count() == 0) 
            {
                return NotFound($"Não existem alunos com esse critério: {name}");
            }

            return Ok(membros);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter o membro pelo nome");
        }
    }

    [HttpGet("{id:int}", Name= "GetById")]

    public async Task<ActionResult<Membro>> GetById(int id)
    {
        try
        {
            var membro = await _membroService.GetById(id);

            if (membro == null)
            {
                return NotFound($"Não existem alunos com esse critério: {id}");
            }

            return Ok(membro);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao obter o membro pelo id");
        }
    }

    [HttpPost]

    public async Task<ActionResult> Create(Membro membro)
    {
        try
        {
            await _membroService.CreateMembro(membro);

            return CreatedAtRoute(nameof(GetById), new {id = membro.Id}, membro);
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao criar um novo membro");
        }
    }

    [HttpPut("{id:int}")]

    public async Task<ActionResult> Edit(int id, [FromBody] Membro membro)
    {
        try
        {
            if(membro.Id == id)
            {
                await _membroService.UptadeMembro(membro);
                return Ok($"Membro com id = {id} foi atualizado com suscesso!");
            }
            else
            {
                return BadRequest("Dados inconsistentes");
            }
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao editar o membro");
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult> Delete(int id)
    {
        try
        {
            var membro = await _membroService.GetById(id);

            if (membro != null)
            {
                await _membroService.DeleteMembro(membro);
                return Ok($"Membro com id = {id} foi excluido com suscesso!");
            }
            else
            {
                return NotFound($"Membro com o id={id} não encontrado");
            }
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError, "Erro ao deletar o membro");
        }
    }
}
