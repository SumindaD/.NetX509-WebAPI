using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SurityTrial.Context;
using SurityTrial.Models;

namespace SurityTrial.DAL
{
    public class ImageUploadSessionRepository : IImageUploadSessionRepository
    {
        public void Add(ImageUploadSession imageUploadSession)
        {
            using (var surityDBContext = new SurityDBContext()) 
            {
                surityDBContext.ImageUploadSessions.Add(imageUploadSession);
                surityDBContext.SaveChanges();
            }
        }

        public List<Image> SearchImages(string requestNumber, string userName)
        {
            using (var surityDBContext = new SurityDBContext()) 
            {
                return surityDBContext.ImageUploadSessions.Include("image").Where(i =>
                        i.RequestNumber == requestNumber &&
                        i.UserName == userName
                    ).Select(x => x.Image).ToList();
            }
        }
    }
}