using FINS.DTO;

namespace FINS.Features.Accounting.AccountGroups
{
    /// <summary>
    /// Category of the Account
    /// </summary>
    public class AccountGroupDto : BaseDto<int>
    {
        /// <summary>
        /// Name of Account Group
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Display Name of Account Group
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Parent child relationship Id AG => AG
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// Is Primary flag
        /// </summary>
        public bool IsPrimary { get; set; }
    }
}
