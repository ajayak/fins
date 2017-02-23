using System;
using System.Collections.Generic;
using FINS.Models.Generated.Person;

namespace FINS.Models.Generated.Purchasing
{
    public class Vendor
    {
        public int BusinessEntityId { get; set; }
        public string AccountNumber { get; set; }
        public string Name { get; set; }
        public byte CreditRating { get; set; }
        public bool PreferredVendorStatus { get; set; }
        public bool ActiveFlag { get; set; }
        public string PurchasingWebServiceUrl { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductVendor> ProductVendor { get; set; } = new HashSet<ProductVendor>();
        public virtual ICollection<PurchaseOrderHeader> PurchaseOrderHeader { get; set; } = new HashSet<PurchaseOrderHeader>();
        public virtual BusinessEntity BusinessEntity { get; set; }
    }
}
