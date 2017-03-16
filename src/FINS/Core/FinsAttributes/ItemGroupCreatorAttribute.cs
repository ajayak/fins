using FINS.Security;
using Microsoft.AspNetCore.Authorization;

namespace FINS.Core.FinsAttributes
{
    public class ItemGroupCreatorAttribute : AuthorizeAttribute
    {
        public ItemGroupCreatorAttribute() : base(Inventory.ItemGroupManager.ToString())
        {

        }
    }
}
