
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
        public const int FaLanguageId = 1;
        public const string EnUsCulture = "en-US";
        public const int EnLanguageId = 2;
        public const string ArIqCulture = "ar-IQ";
        public const int ArLanguageId = 3;
        public const string PrsAfCulture = "prs-AF";
        public const int AfLanguageId = 4;

        #endregion

        public static string TempDataMessageTitle => "Message";

        public const double MaxTransportCoLogoFileSizeUpload = 3e+6; // 3MB
    }
}
