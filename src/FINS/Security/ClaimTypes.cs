namespace FINS.Security
{
    /// <summary>
    /// Custom claims for FINS
    /// </summary>
    public class ClaimTypes
    {
        /// <summary>
        /// The id of the organization
        /// </summary>
        public const string Organization = "fs:organizationid";

        /// <summary>
        /// The display name of the user
        /// </summary>
        public const string DisplayName = "fs:displayname";

        /// <summary>
        /// The type of user
        /// </summary>
        public const string UserType = "fs:usertype";

        /// <summary>
        /// Access levels of user
        /// </summary>
        public const string AccessLevel = "fs:accessLevel";

        /// <summary>
        /// Access levels of user
        /// </summary>
        public const string Accounting = "fs:accounting";
    }
}
