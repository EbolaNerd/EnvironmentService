using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentService.Models
{
    public class EnvironmentContext : DbContext
    {
        public DbSet<Environment> Environments { get; set; }
        
        public EnvironmentContext(DbContextOptions<EnvironmentContext> options)
            :base(options)
        {
            Database.EnsureCreated();
        }

    }
}
