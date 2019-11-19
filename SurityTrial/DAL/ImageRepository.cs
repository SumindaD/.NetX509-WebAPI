using SurityTrial.Context;
using SurityTrial.DAL.Interfaces;
using SurityTrial.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SurityTrial.DAL
{
    public class ImageRepository : IImageRepository
    {
        public Image GetImage(int id)
        {
            using (var surityDBContext = new SurityDBContext()) 
            {
                return surityDBContext.Images.Where(x => x.Id == id).FirstOrDefault();
            }
        }
    }
}