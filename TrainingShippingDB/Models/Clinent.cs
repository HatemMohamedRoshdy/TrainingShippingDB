using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingShippingDB.Models
{
    public class Client
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public string TaxNumber { get; set; }
    }
}