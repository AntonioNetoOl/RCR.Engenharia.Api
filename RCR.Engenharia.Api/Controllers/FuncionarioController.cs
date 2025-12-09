using Microsoft.AspNetCore.Mvc;
using RCR.Engenharia.Sgrh.Application.Funcionarios;

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
            var id = await _funcionarioService.CriarAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetById), new { id }, null);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] FuncionarioDto dto, CancellationToken cancellationToken)
        {
            await _funcionarioService.AtualizarAsync(id, dto, cancellationToken);
            return NoContent();
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            await _funcionarioService.RemoverAsync(id, cancellationToken);
            return NoContent();
        }
    }
}

