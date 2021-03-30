using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animal_Store.Models
{
    public class wish_list
    {
        public int PetsId { get; set; }
        public Pets pet { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}
