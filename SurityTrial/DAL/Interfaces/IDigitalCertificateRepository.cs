using SurityTrial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurityTrial.DAL.Interfaces
{
    public interface IDigitalCertificateRepository
    {
        List<DigitalCertificate> Query();
    }
}