using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SurityTrial.Models;

namespace SurityTrial.DAL.Interfaces
{
    public interface IImageRepository
    {
        Image GetImage(int id);
    }
}