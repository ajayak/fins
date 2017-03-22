namespace FINS.Models.Common
{
    public class State : BaseModel<int>, ISoftDelete, IBelongToOrganization
    {
        /// <summary>
        /// Name of the state
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// State code
        /// </summary>
        public string Code { get; set; }

        public bool IsDeleted { get; set; }
        public int OrganizationId { get; set; }
    }
}
