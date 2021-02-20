using System;
using System.Collections.Generic;

namespace CRUDApp.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderID { get; set; }

        public Provider Provider { get; set; }
        public List<OrderItem> OrderItems{ get; set; }
    }
}