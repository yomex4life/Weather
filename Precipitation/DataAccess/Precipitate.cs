using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Precipitation.DataAccess
{
    public class Precipitate : IPrecipitate
    {
        private readonly PrecipDbContext _context;

        public Precipitate(PrecipDbContext context)
        {
            _context = context;
        }
        public async Task<Precipitation> Get(Guid id)
        {
            return await _context.Precipitations.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<Precipitation> Get(string zip)
        {
            return await _context.Precipitations.FirstOrDefaultAsync(p => p.ZipCode == zip);
        }

        public async Task<IEnumerable<Precipitation>> GetAll()
        {
            return await _context.Precipitations.ToListAsync();
        }
    }
}