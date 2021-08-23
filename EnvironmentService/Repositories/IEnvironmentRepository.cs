using EnvironmentService.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentService.Repositories
{
    public interface IEnvironmentRepository
    {
        Task<IEnumerable<Environment>> Get();
        Task<Environment> Get(int id);
        Task<Environment> Create(Environment environment);
        Task Update(Environment environment);
        Task Delete(int id);
    }
}
