using FINS.Security;
using Microsoft.AspNetCore.Authorization;

namespace FINS.Core.FinsAttributes
{
    public class AccountGroupCreatorAttribute : AuthorizeAttribute
    {
        public AccountGroupCreatorAttribute() : base(Accounting.AccountGroupManager.ToString())
        {
            
        }
    }
}
