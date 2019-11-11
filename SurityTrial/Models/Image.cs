using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurityTrial.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }
        public string FileName { get; set; }
        public string SignedImageFileName { get; set; }
    }
}