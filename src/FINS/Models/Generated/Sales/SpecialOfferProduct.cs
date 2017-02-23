using System;
using System.Collections.Generic;
using FINS.Models.Generated.Production;

namespace FINS.Models.Generated.Sales
{
    public class SpecialOfferProduct
    {
        public int SpecialOfferId { get; set; }
        public int ProductId { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<SalesOrderDetail> SalesOrderDetail { get; set; } = new HashSet<SalesOrderDetail>();
        public virtual Product Product { get; set; }
        public virtual SpecialOffer SpecialOffer { get; set; }
    }
}
