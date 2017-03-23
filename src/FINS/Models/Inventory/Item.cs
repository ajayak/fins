using System;
using FINS.Models.Common;

namespace FINS.Models.Inventory
{
    public class Item : BaseModel<int>, ISoftDelete
    {
        //[WeightUnitMeasureCode] [nchar] (3) NULL, UnitId Required
        //[Weight] [decimal](8, 2) NULL,    Quantity Required

        public string Name { get; set; }

        public string Code { get; set; }

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
        public short SafetyStockLevel { get; set; }

        /// <summary>
        /// Physical location of the item
        /// </summary>
        public string ReorderPoint { get; set; }

        public decimal StandardCost { get; set; }

        public decimal ListPrice { get; set; }

        public decimal Quantity { get; set; }

        public Size Size { get; set; }

        public decimal Weight { get; set; }

        public short DaysToManufacture { get; set; }

        public DateTime? SellStartDate { get; set; }

        public DateTime? SellEndTime { get; set; }

        /// <summary>
        /// Category/Type of Item
        /// </summary>
        public int ItemGroupId { get; set; }
        public ItemGroup ItemGroup { get; set; }

        /// <summary>
        /// Unit of the Item
        /// </summary>
        public int UnitId { get; set; }
        public Unit Unit { get; set; }

        public bool IsDeleted { get; set; }
    }
}
