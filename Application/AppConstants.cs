
namespace Application
{
    public static class AppConstants
    {
        #region Identity

        // Admin Information

        public static string AdminRole => "Administrator";
        public static string AdminRoleId => "3e2c9b3b-1c5c-41a5-9fe6-9004dcd4b78b";
        public static string HatefAdminUsername => "HatefAdmin";
        public static string HatefAdminUserId => "b0a39202-a221-47c7-9d34-dc4479ec33f2";
        public static string HatefAdminPassword => "AdFiba@1#2$3";

        #endregion

        #region ViewData

        public static string ViewDataTitlePageName = "PageTitle";
        public static string ViewDataControllerName = "ControllerName";
        public static string ViewDataControllerRoute = "ControllerRoute";
        public static string ViewDataActionRoute = "ActionRoute";

        #endregion

        #region System Globalization

        public const string FaIrCulture = "fa-IR";
        public const int FaLanguageLcid = 1065;
        public const string EnUsCulture = "en-US";
        public const int EnLanguageLcid = 1033;
        public const string ArIqCulture = "ar-IQ";
        public const int ArLanguageLcid = 2049;
        public const string PrsAfCulture = "prs-AF";
        public const int AfLanguageLcid = 1164;

        #endregion

        #region Files

        public static string HotelsPhotoPath => "images/Hotels";
        public static string MedicalCentersPhotoPath => "images/MedicalCenters";
        public static string RootFilesPath => $"wwwroot";
        public static string ThumbnailPath => "Thumbnails";

        public const int ThumbNailHeight = 90;
        public const int ThumbnailWidth = 90;

        #endregion

        public static string TempDataMessageTitle => "Message";

        public const double MaxHotelLogoFileSizeUpload = 3e+6; // 3MB
        public const double MaxMedicalCenterPicFileSizeUpload = 3e+6; // 3MB

        #region Methods

        public static string ThrowIfNullOrEmpty(this string str, string paramName, bool allowWhiteSpaces = false)
        {
            if(allowWhiteSpaces)
            {
                return string.IsNullOrEmpty(str)
                    ? throw new ArgumentNullException($"{paramName} can not be null, empty or contain whitespaces !")
                    : str;
            }

            return string.IsNullOrWhiteSpace(str)
                ? throw new ArgumentNullException($"{paramName} can not be null or empty!")
                : str;
        }

        #endregion
    }
}
