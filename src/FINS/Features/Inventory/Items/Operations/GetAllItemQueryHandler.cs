using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using FINS.Context;
using FINS.Core.Configuration;
using FINS.Core.Helpers;
using FINS.Features.Inventory.Items.DTO;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FINS.Features.Inventory.Items.Operations
{
    public class GetAllItemQueryHandler : IAsyncRequestHandler<GetAllItemQuery, PagedResult<ItemListDto>>
    {
        private readonly FinsDbContext _context;
        private readonly FinsPathsSettings _paths;

        public GetAllItemQueryHandler(FinsDbContext context, IOptions<FinsPathsSettings> pathSettings)
        {
            _context = context;
            _paths = pathSettings.Value;
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

            result = SetImageThumbnailPath(message.BaseUrl, result);

            return result.ToPagedResult(message.PageNo, message.PageSize, totalRecordCount);
        }

        private List<ItemListDto> SetImageThumbnailPath(string baseUrl, List<ItemListDto> items)
        {
            var folderPath = _paths.ItemImagePath;
            items.ForEach(item =>
            {
                var itemUrl = item.ImageUrl.Split('.').ToList();
                var fileExtension = itemUrl.Last();
                itemUrl.RemoveAt(itemUrl.Count - 1);
                var thumbnail = $"{string.Join(".", itemUrl)}-thumb.{fileExtension}";
                item.ImageUrl = $"{baseUrl}\\{folderPath}\\{thumbnail}";
            });
            return items;
        }
    }
}

