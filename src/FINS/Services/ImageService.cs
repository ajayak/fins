using System;
using System.IO;
using System.Linq;
using ImageSharp;
using ImageSharp.Processing;

namespace FINS.Services
{
    public class ImageService : IImageService
    {
        public string CompressSaveImage(string imageName, string base64Image, string folderPath)
        {
            var imageBytes = base64Image.Split(',');
            var bytes = Convert.FromBase64String(imageBytes[1]);
            var imagePath = $"{folderPath}/{imageName}";
            var image = new Image(bytes);
            image.MetaData.Quality = 80;
            var imageHeight = image.Height > 1000 ? image.Height / 2 : image.Height;
            var imageWidth = image.Width > 1000 ? image.Width / 2 : image.Width;
            image.Resize(new ResizeOptions
            {
                Size = new Size(imageWidth, imageHeight),
                Mode = ResizeMode.Max
            });
            image.MetaData.ExifProfile = null;
            image.Save(imagePath);
            return imagePath;
        }

        public string CreateImageThumbnail(string imagePath)
        {
            using (var input = File.OpenRead(imagePath))
            {
                var image = new Image(input);
                var imageHeight = image.Height > 400 ? 400 : image.Height;
                var imageWidth = image.Width > 400 ? 400 : image.Width;
                image.Resize(new ResizeOptions
                {
                    Size = new Size(imageWidth, imageHeight),
                    Mode = ResizeMode.Max
                });
                image.MetaData.ExifProfile = null;
                image.MetaData.Quality = 80;
                var path = imagePath.Split('.').ToList();
                var fileExtension = path.Last();
                path.RemoveAt(path.Count - 1);
                var thumbnailPath = $"{string.Join(".", path)}-thumb.{fileExtension}";
                image.Save(thumbnailPath);
                return thumbnailPath;
            }
        }
    }

    public interface IImageService
    {
        string CompressSaveImage(string imageName, string base64Image, string folderPath);
        string CreateImageThumbnail(string imagePath);
    }
}
