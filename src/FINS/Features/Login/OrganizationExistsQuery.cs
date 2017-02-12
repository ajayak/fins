using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace FINS.Features.Login
{
    public class OrganizationExistsQuery : IRequest<bool>
    {
        public OrganizationExistsQuery(string name)
        {
            this.OrganizationName = name;
        }
        public string OrganizationName { get; set; }
    }
}
