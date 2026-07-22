using KartSimulator.DTOs.Pilotos;
using KartSimulator.DTOs.Veiculos;
using KartSimulator.Entities;
using KartSimulator.Repositories.Pilotos;
using Microsoft.AspNetCore.Mvc;

namespace KartSimulator.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PilotoController : ControllerBase
    {
        private readonly IPilotoRepository _repository;
        public PilotoController(IPilotoRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("{id}", Name = "ObterPilotoPorId")]
        public async Task<ActionResult<PilotoResponseDto>> ObterPorIdAsync(int id)
        {
            var piloto = await _repository.ObterPorIdAsync(id);
            if (piloto == null) { return NotFound("Piloto não encontrado."); }
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
            return Ok(response);
        }

        [HttpGet]
        public async Task<ActionResult<List<PilotoResponseDto>>> ObterTodosAsync()
        {
            var pilotos = await _repository.ObterTodosAsync();
            if (pilotos == null) { return NotFound("Nenhum piloto encontrado"); }
            var response = pilotos.Select(p => new PilotoResponseDto
            {
                Id = p.Id,
                Nome = p.Nome,
                Idade = p.Idade,
                CorridasVencidas = p.CorridasVencidas,
                HabilidadeCurvas = p.HabilidadeCurvas,
                Consistencia = p.Consistencia,
                Veiculo = p.Veiculo != null ? new VeiculoResponseDto
                {
                    Id = p.Veiculo.Id,
                    Modelo = p.Veiculo.Modelo,
                    VelocidadeMaxima = p.Veiculo.VelocidadeMaxima
                } : null!
            }).ToList();
            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<PilotoResponseDto>> CriarPilotoAsync(PilotoCreateDto pilotoDto)
        {
            var pilotos = await _repository.ObterTodosAsync();
            bool kartEmUso = pilotos.Any(p => p.VeiculoId == pilotoDto.VeiculoId);
            if (kartEmUso)
            {
                return BadRequest($"O kart de ID {pilotoDto.VeiculoId} já está vinculado a outro piloto.");
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
            var response = new PilotoResponseDto
            {
                Id = pilotoCompleto.Id,
                Nome = pilotoCompleto.Nome,
                Idade = pilotoCompleto.Idade,
                CorridasVencidas = pilotoCompleto.CorridasVencidas,
                HabilidadeCurvas = pilotoCompleto.HabilidadeCurvas,
                Consistencia = pilotoCompleto.Consistencia,
                Veiculo = pilotoCompleto.Veiculo != null ? new VeiculoResponseDto
                {
                    Id = pilotoCompleto.Veiculo.Id,
                    Modelo = pilotoCompleto.Veiculo.Modelo,
                    VelocidadeMaxima = pilotoCompleto.Veiculo.VelocidadeMaxima
                } : null!
            };
            return CreatedAtAction("ObterPorId", new { id = response.Id }, response);
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
            await _repository.AtualizarPilotoAsync(pilotoExiste);
            var response = new PilotoResponseDto
            {
                Id = pilotoExiste.Id,
                Nome = pilotoExiste.Nome,
                Idade = pilotoExiste.Idade,
                CorridasVencidas = pilotoExiste.CorridasVencidas,
                HabilidadeCurvas = pilotoExiste.HabilidadeCurvas,
                Consistencia = pilotoExiste.Consistencia,
                Veiculo = pilotoExiste.Veiculo != null ? new VeiculoResponseDto
                {
                    Id = pilotoExiste.Veiculo.Id,
                    Modelo = pilotoExiste.Veiculo.Modelo,
                    VelocidadeMaxima = pilotoExiste.Veiculo.VelocidadeMaxima
                } : null!
            };
            return Ok(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarPilotoAsync(int id)
        {
            var response = await _repository.DeletarPilotoAsync(id);
            if (response == false) { return NotFound($"Piloto de ID {id} não encontrado."); }
            return NoContent();
        }

    }
}
