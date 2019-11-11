using SurityTrial.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SurityTrial.Context
{
    public class SurityDBContext: DbContext
    {
        public SurityDBContext() : base("name=DefaultConnection")
        {

        }

        public DbSet<ImageUploadSession> ImageUploadSessions { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<DigitalCertificate> DigitalCertificate { get; set; }
    }
}