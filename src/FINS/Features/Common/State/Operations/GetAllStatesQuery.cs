using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

namespace FINS.Features.Common.State.Operations
{
    public class GetAllStatesQuery : IRequest<List<StateDto>>
    {

    }
}
