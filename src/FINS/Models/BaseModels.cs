using System;

namespace FINS.Models
{
    /// <summary>
    /// Base Model with Id property
    /// </summary>
    public class BaseModel
    {
        public int Id { get; set; }
    }

    /// <summary>
    /// class must have OrganizationId
    /// </summary>
    public interface IBelongToOrganization
    {
        int OrganizationId { get; set; }
    }

    /// <summary>
    /// Must have added date and added by user Id
    /// </summary>
    public interface IAudited
    {
        int AddedBy { get; set; }
        DateTime? AddedDate { get; set; }
    }

    /// <summary>
    /// must have all audit columns i.e. added and modified details
    /// </summary>
    public interface IFullyAuditedEntity : IAudited
    {
        int ModifiedBy { get; set; }
        DateTime? ModifiedDate { get; set; }
    }

    /// <summary>
    /// must be soft deleted
    /// </summary>
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
