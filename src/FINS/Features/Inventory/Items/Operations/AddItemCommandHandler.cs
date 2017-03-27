using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.AutoMap;
using FINS.Core.Configuration;
using FINS.Features.Accounting.Accounts.DTO;
using FINS.Features.Accounting.Accounts.Operations;
using FINS.Features.Inventory.Items.DTO;
using FINS.Models.Accounting;
using FINS.Models.Inventory;
using FINS.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FINS.Features.Inventory.Items.Operations
{
    public class AddItemCommandHandler : IAsyncRequestHandler<AddItemCommand, ItemDto>
    {
        private readonly FinsDbContext _context;
        private readonly FinsPathsSettings _paths;
        private readonly IImageService _imageService;

        public AddItemCommandHandler(
            FinsDbContext context,
            IOptions<FinsPathsSettings> pathSettings,
            IImageService imageService)
        {
            _context = context;
            _paths = pathSettings.Value;
            _imageService = imageService;
        }

        public async Task<ItemDto> Handle(AddItemCommand query)
        {
            var item = query.MapTo<Item>();
            if (!string.IsNullOrEmpty(query.Base64Image))
            {
                var fileExtension = item.DisplayImageName.Split('.').Last();
                var guid = Guid.NewGuid();
                item.ImageName = $"{guid}.{fileExtension}";
                var folderPath = _paths.ItemImagePath;
                var imagePath = _imageService.CompressSaveImage(item.ImageName, query.Base64Image, folderPath);
                _imageService.CreateImageThumbnail(imagePath);
            }
            else
            {
                item.ImageName = null;
                item.DisplayImageName = null;
            }
            await _context.Items.AddAsync(item);
            await _context.SaveChangesAsync();
            return item.MapTo<ItemDto>();
        }
    }
}
