using System;
using System.Collections.Generic;
using FINS.Models.Generated.Person;

namespace FINS.Models.Generated.Sales
{
    public class Store
    {
        public int BusinessEntityId { get; set; }
        public string Name { get; set; }
        public int? SalesPersonId { get; set; }
        public string Demographics { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Customer> Customer { get; set; } = new HashSet<Customer>();
        public virtual BusinessEntity BusinessEntity { get; set; }
        public virtual SalesPerson SalesPerson { get; set; }
    }
}
