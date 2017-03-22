using System.Collections.Generic;
using FINS.DTO;
using MediatR;

namespace FINS.Features.Common.Unit.Operations
{
    public class GetAllUnitsQuery : IRequest<List<NameCodeDto<int>>>
    {
        public int OrganizationId { get; set; }
    }
}
