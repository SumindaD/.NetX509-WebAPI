using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurityTrial.Models
{

    public class ImageUploadSession
    {
        [Key]
        public int Id { get; set; }
        public string RequestNumber { get; set; }
        public string UserName { get; set; }
        public Image Image { get; set; }
    }
}