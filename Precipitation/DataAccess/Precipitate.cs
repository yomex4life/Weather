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

        public void Add(Precipitation precip)
        {
            if (precip == null)
            {
                throw new ArgumentNullException(nameof(precip));
            }

            _context.Precipitations.Add(precip);
            SaveChangesAsync();
        }

        public async Task<Precipitation> Get(Guid id)
        {
            return await _context.Precipitations.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task<IEnumerable<Precipitation>> GetAll()
        {
            return await _context.Precipitations.ToListAsync();
        }

        public bool SaveChangesAsync()
        {
            return (_context.SaveChanges() >= 0);
        }

        public async Task<IEnumerable<Precipitation>> Get(string zip)
        {
            return await _context.Precipitations.Where(p => p.ZipCode == zip).ToListAsync();
        }
    }
}