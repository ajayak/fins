namespace FINS.Models.Account
{
    /// <summary>
    /// Person Details
    /// </summary>
    public class Person : BaseModel
    {
        /// <summary>
        /// First Name of the contact person
        /// </summary>
        public string FirstName { get; set; }
        
        /// <summary>
        /// Last Name of the contact person
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Email Id of the contact person
        /// </summary>
        public string EmailId { get; set; }

        /// <summary>
        /// Telephone Number
        /// </summary>
        public long Telephone { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        public long Mobile { get; set; }

        /// <summary>
        /// Address
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// State
        /// </summary>
        public string State { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Ward { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ItPanNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string LstNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string CstNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string TinNumber { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string ServiceTaxNumber { get; set; }

        /// <summary>
        /// Associated Account Id
        /// </summary>
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
