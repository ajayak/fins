using FINS.Security;
using Microsoft.AspNetCore.Authorization;

namespace FINS.Core.FinsAttributes
{
    public class ItemCreatorAttribute : AuthorizeAttribute
    {
        public ItemCreatorAttribute() : base(Inventory.ItemManager.ToString())
        {

        }
    }
}
