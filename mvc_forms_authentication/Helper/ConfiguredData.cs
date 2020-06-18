using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace mvc_forms_authentication.Helper
{
    public class ConfiguredData
    {
        public static string Email = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmail"];
        public static string Password = System.Web.Configuration.WebConfigurationManager.AppSettings["SenderEmailPassword"];

    }
}