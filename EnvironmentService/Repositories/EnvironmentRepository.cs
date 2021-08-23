using EnvironmentService.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentService.Repositories
{
    public class EnvironmentRepository : IEnvironmentRepository
    {
        private readonly EnvironmentContext _context;
        public EnvironmentRepository(EnvironmentContext context)
        {
            this._context = context;
        }

        public async Task<Models.Environment> Create(Models.Environment environment)
        {
            this._context.Environments.Add(environment);
            await this._context.SaveChangesAsync();
            
            return environment;
        }

        public async Task Delete(int id)
        {
            var environmentToDelete = await this._context.Environments.FindAsync(id);
            this._context.Environments.Remove(environmentToDelete);
            await this._context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Models.Environment>> Get()
        {
            return await this._context.Environments.ToListAsync();
        }

        public async Task<Models.Environment> Get(int id)
        {
            return await this._context.Environments.FindAsync(id);
        }

        public async Task Update(Models.Environment environment)
        {
            this._context.Entry(environment).State = EntityState.Modified;
            await this._context.SaveChangesAsync();
        }
    }
}
