using System;
using System.Collections.Generic;

namespace FINS.Models.Generated.Person
{
    public class ContactType
    {
        public int ContactTypeId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<BusinessEntityContact> BusinessEntityContact { get; set; } = new HashSet<BusinessEntityContact>();
    }
}
