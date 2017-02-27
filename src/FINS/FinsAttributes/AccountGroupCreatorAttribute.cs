using FINS.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace FINS.FinsAttributes
{
    public class AccountGroupCreatorAttribute : AuthorizeAttribute
    {
        public AccountGroupCreatorAttribute() : base(FinsConstants.AccountGroupCreator)
        {
            
        }
    }
}
