using MediatR;

namespace FINS.Features.Login.Operations
{
    public class GetOrganizationIdQuery : IRequest<int?>
    {
        public string OrganizationName { get; set; }
    }
}
