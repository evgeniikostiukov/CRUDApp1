﻿using System;

namespace CRUDApp.Models
{
    public class Order
    {
        public int ID { get; set; }
        public string Number { get; set; }
        public DateTime Date { get; set; }
        public int ProviderID { get; set; }
    }
}