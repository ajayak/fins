using System;
using System.Collections.Generic;

namespace FINS.Models.Generated.HumarResources
{
    public class Department
    {
        public short DepartmentId { get; set; }
        public string Name { get; set; }
        public string GroupName { get; set; }
        public DateTime ModifiedDate { get; set; }

        public virtual ICollection<EmployeeDepartmentHistory> EmployeeDepartmentHistory { get; set; } = new HashSet<EmployeeDepartmentHistory>();
    }
}
