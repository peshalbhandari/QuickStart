using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickStart.API.Models
{
    public class School
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string State { get; set; }

        public ICollection<Teacher> Teachers { get; set; }
    }
}
