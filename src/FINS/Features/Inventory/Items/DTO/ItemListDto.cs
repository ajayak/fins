using FINS.DTO;

namespace FINS.Features.Inventory.Items.DTO
{
    public class ItemListDto : BaseDto<int>
    {
        /// <summary>
        /// Name of the account
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Account Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Account Group Name
        /// </summary>
        public string ItemGroupName { get; set; }

        public string ImageUrl { get; set; }
    }
}
