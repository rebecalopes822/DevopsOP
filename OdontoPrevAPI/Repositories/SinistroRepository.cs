using OdontoPrevAPI.Data;
using OdontoPrevAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace OdontoPrevAPI.Repositories
{
    public class SinistroRepository
    {
        private readonly OdontoPrevContext _context;

        public SinistroRepository(OdontoPrevContext context)
        {
            _context = context;
        }

        public async Task<List<Sinistro>> GetAll()
        {
            return await _context.Sinistros.Include(s => s.Paciente).Include(s => s.Tratamento).ToListAsync();
        }

        public async Task<Sinistro> GetById(int id)
        {
            return await _context.Sinistros.Include(s => s.Paciente).Include(s => s.Tratamento).FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task Add(Sinistro sinistro)
        {
            _context.Sinistros.Add(sinistro);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Sinistro sinistro)
        {
            _context.Sinistros.Update(sinistro);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var sinistro = await _context.Sinistros.FindAsync(id);
            if (sinistro != null)
            {
                _context.Sinistros.Remove(sinistro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ExisteSinistroParaPaciente(int pacienteId)
        {
            var count = await _context.Sinistros.CountAsync(s => s.PacienteId == pacienteId);
            return count > 0;
        }
    }
}
