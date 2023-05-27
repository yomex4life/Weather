using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Precipitation.DataAccess
{
    public interface IPrecipitate
    {
        Task<IEnumerable<Precipitation>> GetAll();

        Task<Precipitation> Get(Guid id);

        Task<Precipitation> Get(string zip);

        void Add(Precipitation precip);

        bool SaveChangesAsync();
    }
}