using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EnvironmentService.Models
{
    public class Environment
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public string Segment { get; set; }
    }
}
