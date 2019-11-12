using SurityTrial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurityTrial.DAL
{
    public interface IImageUploadSessionRepository
    {
        void Add(ImageUploadSession imageUploadSession);

        List<Image> SearchImages(string requestNumber, string userName);
    }
}