using System;
using System.Collections.Generic;
using FINS.Models.Generated.Purchasing;

namespace FINS.Models.Generated.Production
{
    public class UnitMeasure
    {
        public string UnitMeasureCode { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<BillOfMaterials> BillOfMaterials { get; set; } = new HashSet<BillOfMaterials>();
        public virtual ICollection<Product> ProductSizeUnitMeasureCodeNavigation { get; set; } = new HashSet<Product>();
        public virtual ICollection<Product> ProductWeightUnitMeasureCodeNavigation { get; set; } = new HashSet<Product>();
        public virtual ICollection<ProductVendor> ProductVendor { get; set; } = new HashSet<ProductVendor>();
    }
}
