using Microsoft.EntityFrameworkCore;
using OdontoPrevAPI.Data;
using OdontoPrevAPI.Models;

namespace OdontoPrevAPI.Repositories
{
    public class PacienteRepository
    {
        private readonly OdontoPrevContext _context;

        public PacienteRepository(OdontoPrevContext context)
        {
            _context = context;
        }

        public async Task<List<Paciente>> GetAll()
        {
            return await _context.Pacientes.Include(p => p.Sinistros).ToListAsync();
        }

        public async Task<Paciente> GetById(int id)
        {
            return await _context.Pacientes.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task Add(Paciente paciente)
        {
            _context.Pacientes.Add(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Paciente paciente)
        {
            _context.Pacientes.Update(paciente);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var paciente = await _context.Pacientes.FindAsync(id);
            if (paciente != null)
            {
                _context.Pacientes.Remove(paciente);
                await _context.SaveChangesAsync();
            }
        }
    }
}
