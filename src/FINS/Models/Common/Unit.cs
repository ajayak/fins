using System;

namespace FINS.Models.Common
{
    public class Unit : BaseModel<int>, IFullyAuditedEntity, ISoftDelete, IBelongToOrganization
    {
        public string Name { get; set; }
        public string Code { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public bool IsDeleted { get; set; }
        public int OrganizationId { get; set; }
    }
}
