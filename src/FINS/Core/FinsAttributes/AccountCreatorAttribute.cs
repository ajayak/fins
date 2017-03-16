using FINS.Security;
using Microsoft.AspNetCore.Authorization;

namespace FINS.Core.FinsAttributes
{
    public class AccountCreatorAttribute : AuthorizeAttribute
    {
        public AccountCreatorAttribute() : base(Accounting.AccountManager.ToString())
        {

        }
    }
}
