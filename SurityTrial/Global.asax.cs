using SurityTrial.Configuration;
using SurityTrial.DAL;
using SurityTrial.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.SessionState;
using Unity;
using Unity.WebApi;

namespace SurityTrial
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            GlobalConfiguration.Configure(Config.Register);
            AreaRegistration.RegisterAllAreas();

            // Unity configuration
            var container = new UnityContainer();
            container.RegisterType<IImageUploadSessionRepository, ImageUploadSessionRepository>();
            container.RegisterType<IDigitalCertificateRepository, DigitalCertificateRepository>();
            GlobalConfiguration.Configuration.DependencyResolver = new UnityResolver(container);

            GlobalConfiguration.Configuration.DependencyResolver = new UnityDependencyResolver(container);

            bool directoryExists = System.IO.Directory.Exists(FilePaths.XMLBasePath);

            if (!directoryExists)
                System.IO.Directory.CreateDirectory(FilePaths.XMLBasePath);
        }
    }
}