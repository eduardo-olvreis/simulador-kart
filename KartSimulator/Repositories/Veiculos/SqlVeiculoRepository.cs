using KartSimulator.Data;
using KartSimulator.Entities;
using Microsoft.EntityFrameworkCore;

namespace KartSimulator.Repositories.Veiculos
{
    public class SqlVeiculoRepository : IVeiculoInterface
    {
        private readonly AppDbContext _context;
        public SqlVeiculoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Veiculo?> ObterPorIdAsync(int id)
        {
            return await _context.Veiculos.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Veiculo>> ObterTodosAsync()
        {
            return await _context.Veiculos.AsNoTracking().ToListAsync();
        }

        public async Task<Veiculo> CriarVeiculoAsync(Veiculo veiculo)
        {
            _context.Veiculos.Add(veiculo);
            await _context.SaveChangesAsync();
            return veiculo;
        }

        public async Task<Veiculo> AtualizarVeiculoAsync(Veiculo veiculo)
        {
            if(await ObterPorIdAsync(veiculo.Id) == null) { return null; }
            _context.Update(veiculo);
            await _context.SaveChangesAsync();
            return veiculo;
        }

        public async Task<bool> DeletarVeiculoAsync(int id)
        {
            var veiculo = await _context.Veiculos.FindAsync(id);
            if(veiculo == null) { return false; }
            _context.Remove(veiculo);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
