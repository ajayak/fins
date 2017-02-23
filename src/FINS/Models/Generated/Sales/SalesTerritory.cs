using System;
using System.Collections.Generic;
using FINS.Models.Generated.Person;

namespace FINS.Models.Generated.Sales
{
    public class SalesTerritory
    {
        public int TerritoryId { get; set; }
        public string Name { get; set; }
        public string CountryRegionCode { get; set; }
        public string Group { get; set; }
        public decimal SalesYtd { get; set; }
        public decimal SalesLastYear { get; set; }
        public decimal CostYtd { get; set; }
        public decimal CostLastYear { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Customer> Customer { get; set; } = new HashSet<Customer>();
        public virtual ICollection<SalesOrderHeader> SalesOrderHeader { get; set; } = new HashSet<SalesOrderHeader>();
        public virtual ICollection<SalesPerson> SalesPerson { get; set; } = new HashSet<SalesPerson>();
        public virtual ICollection<SalesTerritoryHistory> SalesTerritoryHistory { get; set; } = new HashSet<SalesTerritoryHistory>();
        public virtual ICollection<StateProvince> StateProvince { get; set; } = new HashSet<StateProvince>();
        public virtual CountryRegion CountryRegionCodeNavigation { get; set; }
    }
}
