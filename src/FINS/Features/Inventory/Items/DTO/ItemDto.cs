using System;
using FINS.DTO;
using FINS.Models;

namespace FINS.Features.Inventory.Items.DTO
{
    public class ItemDto : BaseDto<int>
    {
        public string Name { get; set; }

        public string Code { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Whether item is manufactured by seller
        /// </summary>
        public bool IsSelfMade { get; set; }

        /// <summary>
        /// Is the item a finished good
        /// </summary>
        public bool IsFinishedGood { get; set; }

        /// <summary>
        /// Item Color
        /// </summary>
        public string Color { get; set; }

        /// <summary>
        /// Indicates minimum number of items at safety levely
        /// </summary>
        public short? SafetyStockLevel { get; set; }

        /// <summary>
        /// Physical location of the item
        /// </summary>
        public string ReorderPoint { get; set; }

        public decimal? StandardCost { get; set; }

        public decimal? ListPrice { get; set; }

        public decimal Quantity { get; set; }

        public Size Size { get; set; }

        public decimal? Weight { get; set; }

        public short? DaysToManufacture { get; set; }

        public DateTime? SellStartDate { get; set; }

        public DateTime? SellEndTime { get; set; }

        public string DisplayImageName { get; set; }
        public string Base64Image { get; set; }

        public int ItemGroupId { get; set; }

        public int UnitId { get; set; }
    }
}
