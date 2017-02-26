using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class AccountGroupExistsInOrganizationQuery : IRequest<bool>
    {
        public int ParentAccountGroupId { get; set; }
        public string AccountGroupName { get; set; }
        public int OrganizationId { get; set; } 
    }
}
