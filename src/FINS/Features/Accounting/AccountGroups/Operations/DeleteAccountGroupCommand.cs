using MediatR;

namespace FINS.Features.Accounting.AccountGroups.Operations
{
    public class DeleteAccountGroupCommand : IRequest<bool>
    {
        public int AccountGroupId { get; set; }
        public int OrganizationId { get; set; }
    }
}
