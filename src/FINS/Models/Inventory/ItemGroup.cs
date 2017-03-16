using FINS.Models.App;

namespace FINS.Models.Inventory
{
    public class ItemGroup : BaseModel<int>, ISoftDelete, IBelongToOrganization
    {
        /// <summary>
        /// Name of Item
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display Name of Item
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
        /// Soft delete Item
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Tenant Id which holds this Item
        /// </summary>
        public int OrganizationId { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
