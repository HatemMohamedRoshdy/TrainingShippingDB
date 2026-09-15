using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingShippingDB.Models
{
    public class Bill
    {
        public int Id { get; set; } 
        public string BillNumber { get; set; }
        public int ClientID { get; set; }
        public decimal GrossWeight { get; set; }
        public decimal NetWeight { get; set; }
        public int VoyageID { get; set; }
    }
}