using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application;
using Application.Extensions;
using Application.Interfaces.Application;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace TabanMed.Infrastructure.Services.Application
{
    [System.Runtime.Versioning.SupportedOSPlatform("windows")]
    public class FileManagerService : IFileManagerService
    {
        private readonly ILogger<FileManagerService> _logger;

        public FileManagerService(ILogger<FileManagerService> logger)
        {
            _logger = logger;
        }

        public async Task<string?> SaveFileAsync(IFormFile file, string path = "public", bool overwrite = false,
            bool saveThumbnail = false,
            CancellationToken cancellationToken = new())
        {
            try
            {
                var fileName = Guid.NewGuid().ToString("N");
                var extension = Path.GetExtension(file.FileName);
                var filePath = Path.Combine(path, string.Concat(fileName, extension));
                var mainImagePath = await BaseStoreAsync(file.OpenReadStream(), filePath, cancellationToken);
                if(saveThumbnail)
                    await SaveThumbnailAsync(file, path, string.Concat(fileName, extension));

                return mainImagePath;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "error save file");
                return null;
            }
        }

        private async Task SaveThumbnailAsync(IFormFile file, string path, string fileNameAndExtension)
        {
            using var imageStream = Image.FromStream(file.OpenReadStream());
            using var resizedImage =
                await ResizeImage(imageStream, AppConstants.ThumbnailWidth, AppConstants.ThumbNailHeight);

            resizedImage.Save(Path.Combine(AppConstants.RootFilesPath, path,
                AppConstants.ThumbnailPath, fileNameAndExtension));
        }

        public async Task<bool> DeleteFileAsync(string path, bool deleteThumbnail = false)
        {
            path.ThrowIfNullOrEmpty(nameof(path));

            if(!File.Exists(path))
                return false;

            try
            {
                File.Delete(path);
                if(deleteThumbnail)
                    await DeleteThumbnail(path);

                return true;
            }
            catch(Exception e)
            {
                _logger.LogError(e, "file manager delete failed!");
            }

            return false;
        }

        private Task DeleteThumbnail(string path)
        {
            var fileName = Path.GetFileName(path);
            var thumbNailPath = Path.Combine(Path.GetDirectoryName(path)!, AppConstants.ThumbnailPath, fileName);
            if(File.Exists(thumbNailPath))
                File.Delete(thumbNailPath);
            return Task.CompletedTask;
        }

        /// <summary>
        /// this is base function called by other storage management methods to write on disk.
        /// </summary>
        /// <param name="stream">file/memory stream</param>
        /// <param name="filePath">path to store the file, contain it's name and extension</param>
        /// <param name="cancellationToken"></param>
        /// <param name="overwrite">overwrite if exist or throw exception</param>
        /// <returns></returns>
        private async Task<string> BaseStoreAsync(Stream stream, string filePath,
            CancellationToken cancellationToken = default,
            bool overwrite = false)
        {
            await using var fs = new FileStream(Path.Combine(AppConstants.RootFilesPath, filePath),
                overwrite ? FileMode.Create : FileMode.CreateNew, FileAccess.Write);

            await stream.CopyToAsync(fs, cancellationToken).ConfigureAwait(false);
            return filePath.ToUnixPath();
        }

        /// <summary>
        /// Resize the image to the specified width and height.
        /// </summary>
        /// <param name="image">The image to resize.</param>
        /// <param name="width">The width to resize to.</param>
        /// <param name="height">The height to resize to.</param>
        /// <returns>The resized image.</returns>
        private Task<Bitmap> ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using var graphics = Graphics.FromImage(destImage);
            graphics.CompositingMode = CompositingMode.SourceCopy;
            graphics.CompositingQuality = CompositingQuality.HighQuality;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            graphics.SmoothingMode = SmoothingMode.HighQuality;
            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

            using var wrapMode = new ImageAttributes();
            wrapMode.SetWrapMode(WrapMode.TileFlipXY);
            graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);

            return Task.FromResult(destImage);
        }
    }
}
