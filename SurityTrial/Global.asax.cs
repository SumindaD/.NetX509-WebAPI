using SurityTrial.Configuration;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using System.Web.SessionState;

namespace SurityTrial
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(Config.Register);

            bool directoryExists = System.IO.Directory.Exists(FilePaths.XMLBasePath);

            if (!directoryExists)
                System.IO.Directory.CreateDirectory(FilePaths.XMLBasePath);
        }
    }
}