using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animal_Store.Models
{
    public class Stores
    {
        public int ID { get; set; }
        public string Location { get; set; }
        public string StoreName { get; set; }
        public ICollection<Pets> Pets { get; set; }
    }
}
