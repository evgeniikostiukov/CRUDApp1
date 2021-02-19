using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApp.Models
{
    public class Provider
    {
        public int ID { get; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
