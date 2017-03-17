using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FINS.Context;
using FINS.Core.Helpers;
using FINS.Features.Inventory.Items.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace FINS.Features.Inventory.Items.Operations
{
    public class GetAllItemQueryHandler : IAsyncRequestHandler<GetAllItemQuery, PagedResult<ItemListDto>>
    {
        private readonly FinsDbContext _context;

        public GetAllItemQueryHandler(FinsDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ItemListDto>> Handle(GetAllItemQuery message)
        {
            var query = _context.Items
                .Where(c =>
                c.ItemGroup.OrganizationId == message.OrganizationId &&
                !c.ItemGroup.IsDeleted &&
                !c.IsDeleted);

            var totalRecordCount = await query.CountAsync();
            var result = await query
                .ProjectTo<ItemListDto>()
                .ApplySort(message.Sort)
                .ApplyPaging(message.PageNo, message.PageSize)
                .ToListAsync();

            return result.ToPagedResult(message.PageNo, message.PageSize, totalRecordCount);
        }
    }
}

