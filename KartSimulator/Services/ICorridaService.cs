using KartSimulator.DTOs.Corridas;

namespace KartSimulator.Services
{
    public interface ICorridaService
    {
        public Task<CorridaResponseDto> SimularCorridaAsync(CorridaCreateDto corridaDto);
        public Task<CorridaResponseDto?> ObterPorIdAsync(int id);
        public Task<List<CorridaResponseDto>> ObterTodosAsync();
    }
}
