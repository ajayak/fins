using System;
using System.Linq;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Core.AutoMap;
using FINS.Core.Configuration;
using FINS.Core.FinsExceptions;
using FINS.Features.Inventory.Items.DTO;
using FINS.Models.Inventory;
using FINS.Services;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace FINS.Features.Inventory.Items.Operations
{
    public class UpdateItemCommandHandler : IAsyncRequestHandler<UpdateItemCommand, ItemDto>
    {
        private readonly FinsDbContext _context;
        private readonly FinsPathsSettings _paths;
        private readonly IImageService _imageService;

        public UpdateItemCommandHandler(
            FinsDbContext context,
            IOptions<FinsPathsSettings> pathSettings,
            IImageService imageService)
        {
            _context = context;
            _paths = pathSettings.Value;
            _imageService = imageService;
        }

        public async Task<ItemDto> Handle(UpdateItemCommand query)
        {
            var existingImageDetails = GetImageDetails(query.Id);
            if (existingImageDetails == null)
            {
                throw new FinsNotFoundException("Item not found");
            }

            var item = query.MapTo<Item>();
            item = ProcessImage(item, existingImageDetails, query.Base64Image);

            _context.Items.Attach(item);
            _context.Entry(item).State = EntityState.Modified;

            await _context.SaveChangesAsync();
            return item.MapTo<ItemDto>();
        }

        private Tuple<string, string> GetImageDetails(int itemId)
        {
            return _context.Items
                .AsNoTracking()
                .Where(c => c.Id == itemId)
                .Select(c => new Tuple<string, string>
                (
                    c.ImageName,
                    c.DisplayImageName
                )).FirstOrDefault();
        }

        private Item ProcessImage(Item item, Tuple<string, string> imageDetails, string base64Image)
        {
            if (string.IsNullOrEmpty(imageDetails.Item2))
            {
                // Existing empty 
                if (!string.IsNullOrEmpty(item.DisplayImageName))
                {
                    // new value => add image
                    item = AddImage(item, base64Image);
                }
                // new empty => Do nothing {}
            }
            else
            {
                // Existing value
                item.ImageName = imageDetails.Item1;
                if (string.IsNullOrEmpty(item.DisplayImageName))
                {
                    // new empty => Delete image
                    item = DeleteImageAndThumb(item, imageDetails.Item1);
                }
                else if (item.DisplayImageName != imageDetails.Item2)
                {
                    // new diff value => Delete existing, add new     
                    var uploadedImageName = item.DisplayImageName;
                    item = DeleteImageAndThumb(item, imageDetails.Item1);
                    item.DisplayImageName = uploadedImageName;
                    item = AddImage(item, base64Image);
                }
                // new same value => Do nothing
            }
            return item;
        }

        private Item AddImage(Item item, string base64Image)
        {
            var fileExtension = item.DisplayImageName.Split('.').Last();
            var guid = Guid.NewGuid();
            item.ImageName = $"{guid}.{fileExtension}";
            var folderPath = _paths.ItemImagePath;
            var imagePath = _imageService.CompressSaveImage(item.ImageName, base64Image, folderPath);
            _imageService.CreateImageThumbnail(imagePath);
            return item;
        }

        private Item DeleteImageAndThumb(Item item, string existingImageName)
        {
            var folderPath = _paths.ItemImagePath;
            var imagePath = $"{folderPath}/{existingImageName}";
            if (System.IO.File.Exists(imagePath))
            {
                System.IO.File.Delete(imagePath);
            }

            var path = imagePath.Split('.').ToList();
            var fileExtension = path.Last();
            path.RemoveAt(path.Count - 1);
            var thumbnailPath = $"{string.Join(".", path)}-thumb.{fileExtension}";
            if (System.IO.File.Exists(thumbnailPath))
            {
                System.IO.File.Delete(thumbnailPath);
            }

            item.DisplayImageName = null;
            item.ImageName = null;
            return item;
        }
    }
}
