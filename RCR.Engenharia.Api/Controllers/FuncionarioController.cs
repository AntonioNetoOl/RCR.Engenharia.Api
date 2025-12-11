using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RCR.Engenharia.Sgrh.Application.Funcionarios;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace RCR.Engenharia.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioService _funcionarioService;

        public FuncionariosController(IFuncionarioService funcionarioService)
        {
            _funcionarioService = funcionarioService;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<FuncionarioDto>>> Get(CancellationToken cancellationToken)
        {
            var result = await _funcionarioService.ListarAsync(cancellationToken);
            return Ok(result);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FuncionarioDto>> GetById(Guid id, CancellationToken cancellationToken)
        {
            var result = await _funcionarioService.ObterPorIdAsync(id, cancellationToken);

            if (result is null)
                return NotFound();

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] FuncionarioDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            var id = await _funcionarioService.CriarAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] FuncionarioDto dto, CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
                return ValidationProblem(ModelState);

            await _funcionarioService.AtualizarAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _funcionarioService.RemoverAsync(id, cancellationToken);
            return NoContent();
        }

        /// <summary>
        /// Cria ou atualiza a foto do funcionário.
        /// </summary>
        [HttpPut("{id:guid}/foto")]
        public async Task<ActionResult> PutFoto(Guid id, IFormFile arquivo, CancellationToken cancellationToken)
        {
            if (arquivo is null || arquivo.Length == 0)
                return BadRequest("Arquivo de imagem inválido.");

            using var ms = new System.IO.MemoryStream();
            await arquivo.CopyToAsync(ms, cancellationToken);
            var bytes = ms.ToArray();

            await _funcionarioService.AtualizarFotoAsync(id, bytes, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Remove a foto do funcionário.
        /// </summary>
        [HttpDelete("{id:guid}/foto")]
        public async Task<ActionResult> DeleteFoto(Guid id, CancellationToken cancellationToken)
        {
            await _funcionarioService.RemoverFotoAsync(id, cancellationToken);
            return NoContent();
        }

        [HttpGet("{id:guid}/foto")]
        public async Task<IActionResult> GetFoto(Guid id, CancellationToken cancellationToken)
        {
            var funcionario = await _funcionarioService.ObterPorIdAsync(id, cancellationToken);

            if (funcionario is null)
                return NotFound();

            if (funcionario.Foto is null || funcionario.Foto.Length == 0)
                return NotFound("Funcionário não possui foto cadastrada.");

            
            // Se depois você quiser suportar PNG, etc., podemos guardar o ContentType no banco.
            return File(funcionario.Foto, "image/jpeg");
        }
    }
}
