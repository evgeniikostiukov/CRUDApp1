using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CRUDApp.Models
{
    public class Provider
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public List<Order> Orders { get; set; }
    }
}
