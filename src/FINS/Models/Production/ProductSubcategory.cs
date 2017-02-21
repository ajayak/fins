using System;
using System.Collections.Generic;

namespace FINS.Models
{
    public class ProductSubcategory
    {
        public int ProductSubcategoryId { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<Product> Product { get; set; } = new HashSet<Product>();
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
