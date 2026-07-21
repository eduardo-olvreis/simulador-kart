using KartSimulator.Data;
using KartSimulator.Entities;
using Microsoft.EntityFrameworkCore;

namespace KartSimulator.Repositories.Pilotos
{
    public class SqlPilotoRepository : IPilotoRepository
    {
        private readonly AppDbContext _context;
        public SqlPilotoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Piloto?> ObterPorIdAsync(int id)
        {
            return await _context.Pilotos.AsNoTracking().Include(p => p.Veiculo).FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<List<Piloto>> ObterTodosAsync()
        {
            return await _context.Pilotos.AsNoTracking().Include(p => p.Veiculo).ToListAsync();
        }

        public async Task<Piloto> CriarPilotoAsync(Piloto piloto)
        {
            _context.Pilotos.Add(piloto);
            await _context.SaveChangesAsync();
            return piloto;
        }

        public async Task<Piloto> AtualizarPilotoAsync(Piloto piloto)
        {
            if(await ObterPorIdAsync(piloto.Id) == null) { return null; }
            _context.Update(piloto);
            await _context.SaveChangesAsync();
            return piloto;
        }

        public async Task<bool> DeletarPilotoAsync(int id)
        {
            var piloto = await _context.Pilotos.FindAsync(id);
            if (piloto == null) { return false; }
            _context.Remove(piloto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
