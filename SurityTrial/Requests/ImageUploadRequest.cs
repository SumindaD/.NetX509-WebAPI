using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurityTrial.Requests
{
    public class ImageUploadRequest
    {
        [Required]
        [MaxLengthAttribute(8)]
        [MinLengthAttribute(8)]
        public string RequestNumber { get; set; }
        [Required]
        public string UserName { get; set; }
        [Required]
        public string ImageName { get; set; }
        [Required]
        public byte[] ImageData { get; set; }
    }
}