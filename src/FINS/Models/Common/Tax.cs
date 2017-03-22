namespace FINS.Models.Common
{
    public class Tax : BaseModel<int>, IBelongToOrganization, ISoftDelete
    {
        public string Category { get; set; }
        public double Percentage { get; set; }
        public int OrganizationId { get; set; }
        public bool IsDeleted { get; set; }
    }
}
