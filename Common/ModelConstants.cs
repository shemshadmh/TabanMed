namespace Common
{
    public static class ModelConstants
    {
        #region Identity

        public static class ApplicationUser
        {
            public const int NameMaxLength = 50;
            public const int FamilyMaxLength = 50;
            public const int ProfilePictureMaxLength = 50;
            public const int IpAddressMaxLength = 16;
            public const int UsernameMaxLength = 20;
            public const int EmailMaxLength = 40;
            public const int PhoneMaxLength = 20;
            public const int PasswordHashMaxLength = 100;
        }

        public static class Role
        {
            public const int DisplayNameMaxLength = 100;
        }

        public static class Permission
        {
            public const int ClaimMaxLength = 80;
            public const int DisplayTextMaxLength = 100;
        }



        #endregion

        #region Destination

        public static class Country
        {
            public const int NameMaxLength = 30;
        }

        public static class City
        {
            public const int NameMaxLength = 30;
        }

        #endregion

        #region Hotel

        public static class Hotel
        {
            public const int NameMaxLength = 30;
            public const int ImageUrlMaxLength = 100;
            public const int AboutMaxLength = 300;
            public const int AddressMaxLength = 150;
            public const int CallInformationMaxLength = 30;
            public const int WebsiteAddressMaxLength = 50;
        }

        public static class HotelFacility
        {
            public const int TitleMaxLength = 30;
        }

        public static class HotelImage
        {
            public const int ImageAltMaxLength = 20;
        }

        #endregion

        #region Localization

        public static class Language
        {
            public const int NameMaxLength = 10;
            public const int IsoNameMaxLength = 7;
        }

        #endregion

        public static class Shared
        {
            public const int GUIDMaxLength = 40;
            public const int ZeroValue = 0;
            public const string NVarCharColumnType = "nvarchar";
            public const string VarCharColumnType = "varchar";
            public const string SmallIntColumnType = "smallint"; // A small integer. Signed range is from -32768 to 32767. Unsigned range is from 0 to 65535. The size parameter specifies the maximum display width (which is 255)
            public const string TinyIntColumnType = "tinyint"; // A very small integer. Signed range is from -128 to 127. Unsigned range is from 0 to 255. The size parameter specifies the maximum display width (which is 255)
            public const string FloatColumnType = "float";
            public const string SmallDatetimeColumnType = "smalldatetime"; // smalldatetime: From January 1, 1900 to June 6, 2079 with an accuracy of 1 minutes
        }
    }
}

