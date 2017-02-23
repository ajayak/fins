using FINS.Models.App;

namespace FINS.Models.Accounting
{
    /// <summary>
    /// Category of the Account
    /// </summary>
    public class AccountGroup : BaseModel<int>, ISoftDelete, IBelongToOrganization
    {
        /// <summary>
        /// Name of Account Group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display Name of Account Group
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Parent child relationship Id AG => AG
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Is Primary flag
        /// </summary>
        public bool IsPrimary { get; set; }

        /// <summary>
        /// Soft delete Account Group
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Tenant Id which holds this Account Group
        /// </summary>
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
