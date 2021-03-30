using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Animal_Store.Models
{
    public class Customer
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Usertype { get; set; }
        public ICollection<wish_list> wish_list { get; set; }

    }
}
