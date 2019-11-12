using SurityTrial.Context;
using SurityTrial.DAL.Interfaces;
using SurityTrial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurityTrial.DAL
{
    public class DigitalCertificateRepository : IDigitalCertificateRepository
    {
        public List<DigitalCertificate> Query()
        {
            using (var surityDBContext = new SurityDBContext()) 
            {
                return surityDBContext.DigitalCertificate.ToList();
            }
        }
    }
}