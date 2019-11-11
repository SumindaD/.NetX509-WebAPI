using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurityTrial.Requests
{
    public class ImageSearchRequest
    {
        [Required]
        [MaxLengthAttribute(8)]
        [MinLengthAttribute(8)]
        public string RequestNumber { get; set; }
        [Required]
        public string UserName { get; set; }
    }
}