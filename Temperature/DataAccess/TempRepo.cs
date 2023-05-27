using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Temperature.DataAccess
{
    public class TempRepo : ITemperature
    {
        private readonly TempDbContext _context;

        public TempRepo(TempDbContext context)
        {
            _context = context;
        }
        public bool Add(Temperature temperature)
        {
            if (temperature == null)
            {
                throw new ArgumentNullException(nameof(temperature));
            }

            _context.Temperatures.Add(temperature);
            return SaveChangesAsync();
        }

        public async Task<Temperature> Get(Guid id)
        {
            return await _context.Temperatures.FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<IEnumerable<Temperature>> Get(string zip)
        {
            return await _context.Temperatures.Where(t => t.Zipcode == zip).ToListAsync();
        }

        public async Task<IEnumerable<Temperature>> GetAll()
        {
            return await _context.Temperatures.ToListAsync();
        }

        public bool SaveChangesAsync()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}