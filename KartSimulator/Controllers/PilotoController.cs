using KartSimulator.DTOs.Pilotos;
using KartSimulator.DTOs.Veiculos;
using KartSimulator.Entities;
using KartSimulator.Repositories.Pilotos;
using KartSimulator.Repositories.Veiculos;
using Microsoft.AspNetCore.Mvc;

namespace KartSimulator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PilotoController : ControllerBase
    {
        private readonly IPilotoRepository _repository;
        private readonly IVeiculoRepository _veiculoRepository;
        public PilotoController(IPilotoRepository repository, IVeiculoRepository veiculoRepository)
        {
            _repository = repository;
            _veiculoRepository = veiculoRepository;
        }

        [HttpGet("{id}", Name = "ObterPilotoPorId")]
        public async Task<ActionResult<PilotoResponseDto>> ObterPorIdAsync(int id)
        {
            var piloto = await _repository.ObterPorIdAsync(id);
            if (piloto == null) { return NotFound("Piloto não encontrado."); }
            var response = MapearParaDto(piloto);
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<PilotoResponseDto>>> ObterTodosAsync()
        {
            var pilotos = await _repository.ObterTodosAsync();
            if (pilotos == null) { return NotFound("Nenhum piloto encontrado"); }
            var response = pilotos.Select(MapearParaDto).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<PilotoResponseDto>> CriarPilotoAsync(PilotoCreateDto pilotoDto)
        {
            var pilotos = await _repository.ObterTodosAsync();
            bool kartEmUso = pilotos.Any(p => p.VeiculoId == pilotoDto.VeiculoId);
            if (kartEmUso)
            {
                return BadRequest($"Kart de ID {pilotoDto.VeiculoId} já está vinculado a outro piloto.");
            }

            var kartExiste = await _veiculoRepository.ObterPorIdAsync(pilotoDto.VeiculoId);
            if (kartExiste == null)
            {
                return NotFound($"Kart de ID {pilotoDto.VeiculoId} não existe no sistema.");
            }

            var piloto = new Piloto
            {
                Nome = pilotoDto.Nome,
                Idade = pilotoDto.Idade,
                HabilidadeCurvas = pilotoDto.HabilidadeCurvas,
                Consistencia = pilotoDto.Consistencia,
                VeiculoId = pilotoDto.VeiculoId,
            }; 
            await _repository.CriarPilotoAsync(piloto);
            var pilotoCompleto = await _repository.ObterPorIdAsync(piloto.Id);
            var response = MapearParaDto(pilotoCompleto);
            return CreatedAtRoute("ObterPilotoPorId", new { id = response.Id }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<PilotoResponseDto?>> AtualizarPilotoAsync([FromBody] PilotoCreateDto pilotoDto, int id)
        {
            var pilotoExiste = await _repository.ObterPorIdAsync(id);
            if(pilotoExiste == null) { return NotFound($"Piloto de ID {id} não encontrado."); }
            pilotoExiste.Nome = pilotoDto.Nome;
            pilotoExiste.Idade = pilotoDto.Idade;
            pilotoExiste.HabilidadeCurvas = pilotoDto.HabilidadeCurvas;
            pilotoExiste.Consistencia = pilotoDto.Consistencia;
            pilotoExiste.VeiculoId = pilotoDto.VeiculoId;
            await _repository.AtualizarPilotoAsync(pilotoExiste);
            var response = MapearParaDto(pilotoExiste);
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPilotoAsync(int id)
        {
            var response = await _repository.DeletarPilotoAsync(id);
            if (response == false) { return NotFound($"Piloto de ID {id} não encontrado."); }
            return NoContent();
        }

        private PilotoResponseDto MapearParaDto (Piloto piloto)
        {
            var response = new PilotoResponseDto
            {
                Id = piloto.Id,
                Nome = piloto.Nome,
                Idade = piloto.Idade,
                CorridasVencidas = piloto.CorridasVencidas,
                HabilidadeCurvas = piloto.HabilidadeCurvas,
                Consistencia = piloto.Consistencia,
                Veiculo = piloto.Veiculo != null ? new VeiculoResponseDto
                {
                    Id = piloto.Veiculo.Id,
                    Modelo = piloto.Veiculo.Modelo,
                    VelocidadeMaxima = piloto.Veiculo.VelocidadeMaxima
                } : null!
            };
            return response;
        }

    }
}
