namespace FINS.Models.Accounting
{
    /// <summary>
    /// Person Details
    /// </summary>
    public class Person : BaseModel<int>
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
        /// Description of the contact person
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Telephone Number
        /// </summary>
        public long Telephone { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        public long Mobile { get; set; }

        /// <summary>
        /// Associated Account Id
        /// </summary>
        public int AccountId { get; set; }
        public virtual Account Account { get; set; }
    }
}
