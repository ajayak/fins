using FINS.Security;
using Microsoft.AspNetCore.Authorization;

namespace FINS.FinsAttributes
{
    public class AccountGroupCreatorAttribute : AuthorizeAttribute
    {
        public AccountGroupCreatorAttribute() : base(Accounting.AccountGroupCreator.ToString())
        {
            
        }
    }
}
