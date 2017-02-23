using System;
using System.Collections.Generic;

namespace FINS.Models.Generated.Sales
{
    public class Customer
    {
        public int CustomerId { get; set; }
        public int? PersonId { get; set; }
        public int? StoreId { get; set; }
        public int? TerritoryId { get; set; }
        public string AccountNumber { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; } = new HashSet<SalesOrderHeader>();
        public virtual Person.Person Person { get; set; }
        public virtual Store Store { get; set; }
        public virtual SalesTerritory Territory { get; set; }
    }
}
