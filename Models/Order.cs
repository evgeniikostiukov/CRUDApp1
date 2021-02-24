using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CRUDApp.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Number { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime Date { get; set; }
        public int ProviderID { get; set; }

        public Provider Provider { get; set; }
        public List<OrderItem> OrderItems{ get; set; }
    }
}