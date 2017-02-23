using System;
using System.Collections.Generic;

namespace FINS.Models.Generated.Person
{
    public class PhoneNumberType
    {
        public int PhoneNumberTypeId { get; set; }
        public string Name { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<PersonPhone> PersonPhone { get; set; } = new HashSet<PersonPhone>();
    }
}
