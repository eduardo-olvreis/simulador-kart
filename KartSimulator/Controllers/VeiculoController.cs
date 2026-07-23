using KartSimulator.DTOs.Veiculos;
using KartSimulator.Entities;
using KartSimulator.Repositories.Veiculos;
using Microsoft.AspNetCore.Mvc;

namespace KartSimulator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoRepository _repository;
        public VeiculoController(IVeiculoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name = "ObterVeiculoPorId")]
        public async Task<ActionResult<VeiculoResponseDto>> ObterPorIdAsync(int id)
        {
            var veiculo = await _repository.ObterPorIdAsync(id);
            if(veiculo == null) { return NotFound($"Veiculo de ID {id} não encontrado."); }
            var response = MapearParaDto(veiculo);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VeiculoResponseDto>>> ObterTodosAsync()
        {
            var veiculos = await _repository.ObterTodosAsync();
            var response = veiculos.Select(MapearParaDto).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<VeiculoResponseDto>> CriarVeiculoAsync(VeiculoCreateDto veiculoDto)
        {
            var veiculo = new Veiculo
            {
                Modelo = veiculoDto.Modelo,
                VelocidadeMaxima = veiculoDto.VelocidadeMaxima
            };
            await _repository.CriarVeiculoAsync(veiculo);
            var veiculoCompleto = await _repository.ObterPorIdAsync(veiculo.Id);
            var response = MapearParaDto(veiculoCompleto);
            return CreatedAtRoute("ObterVeiculoPorId", new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<VeiculoResponseDto>> AtualizarVeiculoAsync(int id, [FromBody] VeiculoCreateDto veiculoDto)
        {
            var veiculoExiste = await _repository.ObterPorIdAsync(id);
            if (veiculoExiste == null) { return NotFound($"Veiculo de ID {id} não encontrado."); }
            veiculoExiste.Modelo = veiculoDto.Modelo;
            veiculoExiste.VelocidadeMaxima = veiculoDto.VelocidadeMaxima;
            await _repository.AtualizarVeiculoAsync(veiculoExiste);
            var response = MapearParaDto(veiculoExiste);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarVeiculoAsync(int id)
        {
            var veiculoExiste = await _repository.ObterPorIdAsync(id);
            if(veiculoExiste == null) { return NotFound($"Veiculo de ID {id} não encontrado."); }
            await _repository.DeletarVeiculoAsync(id);
            return NoContent();
        }

        private VeiculoResponseDto MapearParaDto(Veiculo veiculo)
        {
            var response = new VeiculoResponseDto
            {
                Id = veiculo.Id,
                Modelo = veiculo.Modelo,
                VelocidadeMaxima = veiculo.VelocidadeMaxima
            };
            return response;
        }
    }
}
