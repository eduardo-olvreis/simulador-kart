using KartSimulator.Entities;

namespace KartSimulator.Repositories.Veiculos
{
    public interface IVeiculoRepository
    {
        Task<Veiculo?> ObterPorIdAsync(int id);
        Task<List<Veiculo>> ObterTodosAsync();
        Task<Veiculo> CriarVeiculoAsync(Veiculo veiculo);
        Task<Veiculo> AtualizarVeiculoAsync(Veiculo veiculo);
        Task<bool> DeletarVeiculoAsync(int id);
    }
}
