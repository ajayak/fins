using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.FinsExceptions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Inventory.Items.Operations
{
    public class DeleteItemCommandHandler : IAsyncRequestHandler<DeleteItemCommand, bool>
    {
        private readonly FinsDbContext _context;

        public DeleteItemCommandHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<bool> Handle(DeleteItemCommand query)
        {
            var account = await _context.Items
                .FirstOrDefaultAsync(c => c.Id == query.ItemId &&
                            c.ItemGroup.OrganizationId == query.OrganizationId);
            if (account == null)
            {
                throw new FinsNotFoundException("No matching item found!");
            }
            account.IsDeleted = true;
            return await _context.SaveChangesAsync() > 0;
        }
    }
}
