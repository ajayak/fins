using System;
using System.Collections.Generic;
using FINS.Models.Generated.HumarResources;
using FINS.Models.Generated.Sales;

namespace FINS.Models.Generated.Person
{
    public class Person
    {
        public int BusinessEntityId { get; set; }
        public string PersonType { get; set; }
        public bool NameStyle { get; set; }
        public string Title { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Suffix { get; set; }
        public int EmailPromotion { get; set; }
        public string AdditionalContactInfo { get; set; }
        public string Demographics { get; set; }
        public Guid Rowguid { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<BusinessEntityContact> BusinessEntityContact { get; set; } = new HashSet<BusinessEntityContact>();
        public virtual ICollection<Customer> Customer { get; set; } = new HashSet<Customer>();
        public virtual ICollection<EmailAddress> EmailAddress { get; set; } = new HashSet<EmailAddress>();
        public virtual Employee Employee { get; set; }
        public virtual Password Password { get; set; }
        public virtual ICollection<PersonCreditCard> PersonCreditCard { get; set; } = new HashSet<PersonCreditCard>();
        public virtual ICollection<PersonPhone> PersonPhone { get; set; } = new HashSet<PersonPhone>();
        public virtual BusinessEntity BusinessEntity { get; set; }
    }
}
