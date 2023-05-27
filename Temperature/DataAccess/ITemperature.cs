using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Temperature.DataAccess
{
    public interface ITemperature
    {
        Task<Temperature> Get(Guid id);
        Task<IEnumerable<Temperature>> Get(string zip);
        Task<IEnumerable<Temperature>> GetAll();
        bool Add(Temperature temperature);
        bool SaveChangesAsync();
    }
}