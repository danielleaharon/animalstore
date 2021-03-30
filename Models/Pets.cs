using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animal_Store.Models
{
    public class Pets
    {
        public int ID { get; set; }
        public string Type { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int LifeExpectancy { get; set; }
        public int StoreId { get; set; }
        public Stores Store { get; set; }
        public string img { get; set; }
        public ICollection<wish_list> wish_list { get; set; }
    }
}
