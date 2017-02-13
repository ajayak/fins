using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FINS.Models;
using MediatR;

namespace FINS.Features.Login
{
    public class GetOrganizationIdQuery : IRequest<int?>
    {
        public string OrganizationName { get; set; }
    }
}
