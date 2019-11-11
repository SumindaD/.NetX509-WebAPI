using SurityCryptography;
using SurityTrial.Configuration;
using SurityTrial.Context;
using SurityTrial.Models;
using SurityTrial.Requests;
using SurityTrial.Responses;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SurityTrial.Controllers
{
    public class ImageController : ApiController
    {

        [HttpGet]
        public HttpResponseMessage Search(ImageSearchRequest imageSearchRequest)
        {
            if (ModelState.IsValid) 
            {
                List<Image> imageResult = new List<Image>();
                var imageSearchResponse = new List<ImageSearchResponse>();

                using (var surityDBContext = new SurityDBContext()) 
                {
                    imageResult = surityDBContext.ImageUploadSessions.Include("image").Where(i =>
                            i.RequestNumber == imageSearchRequest.RequestNumber &&
                            i.UserName == imageSearchRequest.UserName
                        ).Select(x => x.Image).ToList();

                    var digitalCertificate = surityDBContext.DigitalCertificate.FirstOrDefault();

                    imageResult.ForEach(x => imageSearchResponse.Add(new ImageSearchResponse 
                    {
                        ImageName = x.FileName,
                        ImageData = DigitalCertificateManager.GetImage(Path.Combine(FilePaths.XMLBasePath, x.SignedImageFileName), digitalCertificate.PublicKey)
                    }));

                    //File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/") + "\\" + "Test_Generated.png", imageSearchResponse.FirstOrDefault().ImageData);
                }

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(imageSearchResponse))
                };
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }

        [HttpPost]
        public HttpResponseMessage Upload(ImageUploadRequest imageUploadRequest)
        {
            if (ModelState.IsValid)
            {
                using (var surityDBContext = new SurityDBContext())
                {
                    var xmlDocumentName = Guid.NewGuid().ToString() + ".xml";

                    var imageUploadSession = new ImageUploadSession()
                    {
                        RequestNumber = imageUploadRequest.RequestNumber,
                        UserName = imageUploadRequest.UserName,
                        Image = new Image()
                        {
                            FileName = imageUploadRequest.ImageName,
                            SignedImageFileName = xmlDocumentName
                        }
                    };

                    var digitalCertificate = surityDBContext.DigitalCertificate.FirstOrDefault();

                    DigitalCertificateManager.SignImage(
                        imageUploadRequest.ImageData,
                        //File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/") + "\\" + "Test.png"),
                        digitalCertificate.PrivateKey,
                        Path.Combine(FilePaths.XMLBasePath, xmlDocumentName)
                    );


                    surityDBContext.ImageUploadSessions.Add(imageUploadSession);
                    surityDBContext.SaveChanges();

                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(LanguageConstants.ImageSuccessfullyUploaded)
                    };
                }
            }
            else
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
            }
        }
    }
}
