using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace GrantApp.StaticValue
{
    public static class ConstantValues
    {
        //public static DateTime SubmissionDateForProduction = DateTime.ParseExact("10/12/2022", "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //public static DateTime SubmissionDateForProduction = DateTime.ParseExact("14/01/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //public static DateTime SubmissionDateForProvinceOnly = DateTime.ParseExact("12/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture);

        public static DateTime ProfileUpdateGreaterThan = DateTime.ParseExact("01/01/2026", "dd/MM/yyyy", CultureInfo.InvariantCulture);


        public static DateTime SubmissionDateForProduction = DateTime.ParseExact("12/02/2026", "dd/MM/yyyy", CultureInfo.InvariantCulture);
        public static DateTime SubmissionDateForProvinceOnly = DateTime.ParseExact("12/02/2026", "dd/MM/yyyy", CultureInfo.InvariantCulture);

        //public static DateTime SubmissionDateForProduction = DateTime.ParseExact("12/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture);
        //public static DateTime SubmissionDateForProvinceOnly = DateTime.ParseExact("12/02/2023", "dd/MM/yyyy", CultureInfo.InvariantCulture);





        public static string SubmissionDateErrorMessage = @"अनुदान प्रविष्ट गर्ने समय समाप्त भयो ।";


    }
}