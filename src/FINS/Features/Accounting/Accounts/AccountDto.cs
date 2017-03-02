using FINS.DTO;

namespace FINS.Features.Accounting.Accounts
{
    public class AccountDto : BaseDto<int>
    {
        /// <summary>
        /// Name of the account
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display name of the Account
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Account Code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Account Group Name
        /// </summary>
        public string AccountGroupName { get; set; }
    }
}
