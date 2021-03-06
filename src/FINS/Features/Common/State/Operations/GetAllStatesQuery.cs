﻿using System.Collections.Generic;
using FINS.DTO;
using MediatR;

namespace FINS.Features.Common.State.Operations
{
    public class GetAllStatesQuery : IRequest<List<NameCodeDto<int>>>
    {
        public int OrganizationId { get; set; }
    }
}
