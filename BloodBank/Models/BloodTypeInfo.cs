using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BloodBank.Models
{
    public class BloodTypeInfo
    {
        public string Id { get; set; }
        public string Area { get; set; }
        public string Type { get; internal set; }
        public string AvailabilityStatus { get; set; }
        public int StockLevel { get; set; }
        public DateTime LastUpdated { get; set; }
        
    }
}
