using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainingShippingDB.Models
{
    public class Voyage
    {
        public int Id { get; set; }
        public string VoyageNumber { get; set; }
        public string VesselName { get; set; }
        public DateTime? ETA { get; set; }
        public DateTime? ETD { get; set; }  
    }
}