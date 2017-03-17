namespace FINS.Models.Inventory
{
    public class Item : BaseModel<int>, ISoftDelete
    {
        /// <summary>
        /// Category/Type of Item
        /// </summary>
        public int ItemGroupId { get; set; }
        public ItemGroup ItemGroup { get; set; }

        public bool IsDeleted { get; set; }
    }
}
