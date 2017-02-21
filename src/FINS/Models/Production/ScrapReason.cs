using System;
using System.Collections.Generic;

namespace FINS.Models
{
    public class ScrapReason
    {
        public short ScrapReasonId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<WorkOrder> WorkOrder { get; set; } = new HashSet<WorkOrder>();
    }
}
