using System;
using System.Collections.Generic;

namespace FINS.Models.Generated.Production
{
    public class Culture
    {
        public string CultureId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<ProductModelProductDescriptionCulture> ProductModelProductDescriptionCulture { get; set; } = new HashSet<ProductModelProductDescriptionCulture>();
    }
}
