﻿using System;
using System.Collections.Generic;

namespace FINS.Models.Generated.Production
{
    public class WorkOrder
    {
        public int WorkOrderId { get; set; }
        public int ProductId { get; set; }
        public int OrderQty { get; set; }
        public int StockedQty { get; set; }
        public short ScrappedQty { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime DueDate { get; set; }
        public short? ScrapReasonId { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<WorkOrderRouting> WorkOrderRouting { get; set; } = new HashSet<WorkOrderRouting>();
        public virtual Product Product { get; set; }
        public virtual ScrapReason ScrapReason { get; set; }
    }
}
