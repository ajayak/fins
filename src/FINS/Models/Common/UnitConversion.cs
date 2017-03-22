using System;

namespace FINS.Models.Common
{
    public class UnitConversion : BaseModel<int>, ISoftDelete, IFullyAuditedEntity
    {
        public int ParentUnitId { get; set; }
        public int SubUnitId { get; set; }
        public double MultiplicationFactor { get; set; }
        public bool IsDeleted { get; set; }
        public string AddedBy { get; set; }
        public DateTime AddedDate { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
