using OdontoPrevAPI.Data;
using OdontoPrevAPI.Models;
using Microsoft.EntityFrameworkCore;


namespace OdontoPrevAPI.Repositories
{
    public class TratamentoRepository
    {
        private readonly OdontoPrevContext _context;

        public TratamentoRepository(OdontoPrevContext context)
        {
            _context = context;
        }

        public async Task<List<Tratamento>> GetAll()
        {
            return await _context.Tratamentos.ToListAsync();
        }

        public async Task<Tratamento> GetById(int id)
        {
            return await _context.Tratamentos.FindAsync(id);
        }

        public async Task Add(Tratamento tratamento)
        {
            _context.Tratamentos.Add(tratamento);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Tratamento tratamento)
        {
            _context.Tratamentos.Update(tratamento);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var tratamento = await _context.Tratamentos.FindAsync(id);
            if (tratamento != null)
            {
                _context.Tratamentos.Remove(tratamento);
                await _context.SaveChangesAsync();
            }
        }
    }
}
