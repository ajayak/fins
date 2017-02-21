using System;
using System.Collections.Generic;

namespace FINS.Models
{
    public class Address
    {
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public int StateProvinceId { get; set; }
        public string PostalCode { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<BusinessEntityAddress> BusinessEntityAddress { get; set; } = new HashSet<BusinessEntityAddress>();
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaderBillToAddress { get; set; } = new HashSet<SalesOrderHeader>();
        public virtual ICollection<SalesOrderHeader> SalesOrderHeaderShipToAddress { get; set; } = new HashSet<SalesOrderHeader>();
        public virtual StateProvince StateProvince { get; set; }
    }
}
