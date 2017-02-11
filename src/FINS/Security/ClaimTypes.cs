using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FINS.Security
{
    /// <summary>
    /// Custom claims for FINS
    /// </summary>
    public class ClaimTypes
    {
        /// <summary>
        /// The display name of the user
        /// </summary>
        public const string DisplayName = "fs:displayname";

        /// <summary>
        /// The type of user
        /// </summary>
        public const string UserType = "fs:usertype";
    }
}
