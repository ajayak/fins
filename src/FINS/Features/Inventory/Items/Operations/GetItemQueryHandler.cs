using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.AutoMap;
using FINS.Features.Inventory.Items.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Inventory.Items.Operations
{
    public class GetItemQueryHandler : IAsyncRequestHandler<GetItemQuery, ItemDto>
    {
        private readonly FinsDbContext _context;

        public GetItemQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<ItemDto> Handle(GetItemQuery query)
        {
            var item = await _context.Items
                .FirstOrDefaultAsync(c =>
                    c.Id == query.ItemId &&
                    c.ItemGroup.OrganizationId == query.OrganizationId);
            return item.MapTo<ItemDto>();
        }
    }
}
