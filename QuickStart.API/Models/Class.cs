using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QuickStart.API.Models
{
    public class Class
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Teacher Teacher { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
