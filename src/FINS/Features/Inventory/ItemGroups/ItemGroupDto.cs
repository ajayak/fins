using System.ComponentModel.DataAnnotations;
using FINS.DTO;

namespace FINS.Features.Inventory.ItemGroups
{
    /// <summary>
    /// Category of the Account
    /// </summary>
    public class ItemGroupDto : BaseDto<int>
    {
        /// <summary>
        /// Name of Item Group
        /// </summary>
        [Required, MaxLength(200)]
        public string Name { get; set; }

        /// <summary>
        /// Display Name of Item Group
        /// </summary>
        [Required, MaxLength(200)]
        public string DisplayName { get; set; }

        /// <summary>
        /// Parent child relationship Id AG => AG
        /// </summary>
        [Required]
        public int ParentId { get; set; }

        /// <summary>
        /// Is Primary flag
        /// </summary>
        public bool IsPrimary { get; set; }
    }
}
