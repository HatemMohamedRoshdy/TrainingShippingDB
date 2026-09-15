using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingShippingDB.Models
{
    public class Container
    {
       public int Id { get; set; }
        public string ContainerNumber { get; set; }
        public string ContainerType { get; set; }
        public int BillID { get; set; }
    }
}