using System.Collections.Generic;
using FINS.Models.App;

namespace FINS.Models.Account
{
    /// <summary>
    /// Account Details
    /// </summary>
    public class Account : BaseModel, IBelongToOrganization, ISoftDelete
    {
        /// <summary>
        /// Name of the account
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the Account
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Category/Type of Account
        /// </summary>
        public int AccountGroupId { get; set; }
        public AccountGroup AccountGroup { get; set; }

        /// <summary>
        /// Opening balance Amount
        /// </summary>
        public decimal OpeningBalance { get; set; }

        /// <summary>
        /// Type of Opening Balance Credit/Debit
        /// </summary>
        public TransactionType OpeningBalanceType { get; set; }

        /// <summary>
        /// Soft delete Account Group
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Tenant Id which holds this Account Group
        /// </summary>
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// List of contact Persons
        /// </summary>
        public virtual ICollection<Person> ContactPersons { get; set; } = new HashSet<Person>();
    }
}
