using System.Collections.Generic;

namespace FINS.Models.App
{
    /// <summary>
    /// Represents tenant
    /// </summary>
    public class Organization : BaseModel, ISoftDelete
    {
        /// <summary>
        /// The name of the organization
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The organization code
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// The URL of the logo used by the organization
        /// </summary>
        public string LogoUrl { get; set; }

        /// <summary>
        /// The URL for the organization's website
        /// </summary>
        public string WebUrl { get; set; }

        /// <summary>
        /// Short summary description of the organzation which can be used in tiles and smaller display areas
        /// </summary>
        public string Summary { get; set; }

        /// <summary>
        /// Html content provided for the description of the organization
        /// </summary>
        public string DescriptionHtml { get; set; }

        /// <summary>
        /// Application users which are members of this Organization.
        /// </summary>
        public List<ApplicationUser> Users { get; set; }

        /// <summary>
        /// The url for an external organization privacy policy document
        /// </summary>
        public string PrivacyPolicyUrl { get; set; }

        /// <summary>
        /// The html for an organization specific privacy policy
        /// </summary>
        public string PrivacyPolicy { get; set; }

        /// <summary>
        /// Soft delete organization
        /// </summary>
        public bool IsDeleted { get; set; }
    }
}
