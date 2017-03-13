using System.Collections.Generic;
using FINS.DTO;
using FINS.Models;
using FINS.Models.Accounting;

namespace FINS.Features.Accounting.Accounts.DTO
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
        /// Category/Type of Account
        /// </summary>
        public int AccountGroupId { get; set; }

        /// <summary>
        /// Opening balance Amount
        /// </summary>
        public decimal OpeningBalance { get; set; }

        /// <summary>
        /// Type of Opening Balance Credit/Debit
        /// </summary>
        public TransactionType OpeningBalanceType { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// State from master table
        /// </summary>
        public int StateId { get; set; }

        /// <summary>
        /// Type of ward/area
        /// </summary>
        public string Ward { get; set; }

        /// <summary>
        /// Pan Number
        /// </summary>
        public string ItPanNumber { get; set; }

        /// <summary>
        /// Local Sales Tax
        /// </summary>
        public string LstNumber { get; set; }

        /// <summary>
        /// Central Sales Tax
        /// </summary>
        public string CstNumber { get; set; }

        /// <summary>
        /// Tax Number
        /// </summary>
        public string TinNumber { get; set; }

        /// <summary>
        /// Service tax number
        /// </summary>
        public string ServiceTaxNumber { get; set; }

        /// <summary>
        /// List of contact Persons
        /// </summary>
        public virtual ICollection<PersonDto> ContactPersons { get; set; } = new HashSet<PersonDto>();
    }
}
