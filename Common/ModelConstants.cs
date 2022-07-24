﻿namespace Common
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

            public static class Shared
        {
            public const int GUIDMaxLength = 40;
            public const string NVarCharColumnType = "nvarchar";
            public const string VarCharColumnType = "varchar";
            public const string SmallIntColumnType = "smallint"; // A small integer. Signed range is from -32768 to 32767. Unsigned range is from 0 to 65535. The size parameter specifies the maximum display width (which is 255)
            public const string TinyIntColumnType = "tinyint"; // A very small integer. Signed range is from -128 to 127. Unsigned range is from 0 to 255. The size parameter specifies the maximum display width (which is 255)
            public const string FloatColumnType = "float";
            public const string SmallDatetimeColumnType = "smalldatetime"; // smalldatetime: From January 1, 1900 to June 6, 2079 with an accuracy of 1 minutes
        }
    }
}

