using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace SurityTrial.Configuration
{
    public static class FilePaths
    {
        public static string XMLBasePath = HttpContext.Current.Server.MapPath("~/" + ConfigurationManager.AppSettings["XMLBasePath"]);
    }
}