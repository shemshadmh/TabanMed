using System.Drawing;
using Microsoft.AspNetCore.Http;

namespace Application.Extensions
{
    public static class FileExtensions
    {
        public static string ToUnixPath(this string path)
        {
            return path.Replace('\\', '/');
        }

        /// <summary>
        /// Returns file size in MB
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public static int GetFileSizeMb(this IFormFile file)
        {
            try
            {
                return Convert.ToInt32(file.Length / 1048576.0);
            }
            catch(Exception)
            {
                return 0;
            }
        }

        private static readonly IDictionary<string, string> ImageMimeDictionary = new Dictionary<string, string>
        {
            { ".bmp", "image/bmp" },
            { ".jpeg", "image/jpeg" },
            { ".jpg", "image/jpeg" },
            { ".png", "image/png" }
        };

        [System.Runtime.Versioning.SupportedOSPlatform("windows")]
        public static bool IsValidImage(this IFormFile image)
        {
            try
            {
                if(!ImageMimeDictionary.ContainsKey(image.GetFileExtension().ToLower())) return false;
                using var imageStream = Image.FromStream(image.OpenReadStream());
                //return true; // it's image
                return true;
            }
            catch(Exception ex) // invalid image file
            {
                Console.WriteLine("Invalid Image File, " + ex.Message);
                return false;
            }
        }

        /// <summary>
        /// extract file extension from <see cref="IFormFile"/> input.
        /// </summary>
        /// <param name="file">the full name/path of the file</param>
        /// <returns>image.png => png</returns>
        public static string GetFileExtension(this IFormFile file)
        {
            return file.FileName.Substring(file.FileName.LastIndexOf('.')).ToLower();
        }
    }
}
