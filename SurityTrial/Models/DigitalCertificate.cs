using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SurityTrial.Models
{
    public class DigitalCertificate
    {
        [Key]
        public int Id { get; set; }
        public byte[] PrivateKey { get; set; }
        public byte[] PublicKey { get; set; }
    }
}