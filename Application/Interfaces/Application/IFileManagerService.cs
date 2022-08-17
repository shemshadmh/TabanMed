
using Microsoft.AspNetCore.Http;

namespace Application.Interfaces.Application
{
    public interface IFileManagerService
    {
        /// <summary>
        /// only save file (no secure) to provided path (public directory by default). useful for public file's like profiles, backgrounds, resources like sliders, categories and ...
        /// </summary>
        /// <param name="file"></param>
        /// <param name="path">storage path (public by default) </param>
        /// <param name="overwrite">overwrite if exist?</param>
        /// <param name="cancellationToken"></param>
        /// <param name="saveThumbnail">if true saves another copy of the photo by 130x90 size in the thumbnails
        /// Directory</param>
        /// <returns>stored file path</returns>
        Task<string> SaveFileAsync(IFormFile file, string path = "public", bool overwrite = false,
            bool saveThumbnail = false,
            CancellationToken cancellationToken = new());

        /// <summary>
        /// delete a file from storage.
        /// </summary>
        /// <param name="path">file path</param>
        /// <param name="deleteThumbnail">if true deletes thumbnailPhoto if Exist</param>
        /// <returns></returns>
        Task<bool> DeleteFileAsync(string path, bool deleteThumbnail = false);
    }
}
