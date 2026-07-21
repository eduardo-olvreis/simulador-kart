using KartSimulator.Entities;

namespace KartSimulator.Repositories.Pilotos
{
    public interface IPilotoRepository
    {
        Task<Piloto?> ObterPorIdAsync(int id);
        Task<List<Piloto>> ObterTodosAsync();
        Task<Piloto> CriarPilotoAsync(Piloto piloto);
        Task<Piloto> AtualizarPilotoAsync(Piloto piloto);
        Task<bool> DeletarPilotoAsync(int id);
    }
}
