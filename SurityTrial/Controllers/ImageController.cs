using SurityCryptography;
using SurityTrial.Configuration;
using SurityTrial.Context;
using SurityTrial.DAL;
using SurityTrial.DAL.Interfaces;
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
        private IImageUploadSessionRepository _imageUploadSessionRepository;
        private IDigitalCertificateRepository _digitalCertificateRepository;


        public ImageController(IImageUploadSessionRepository imageUploadSessionRepository, IDigitalCertificateRepository digitalCertificateRepository) 
        {
            _imageUploadSessionRepository = imageUploadSessionRepository;
            _digitalCertificateRepository = digitalCertificateRepository;
        }


        /// <summary>
        /// Searches images using the provided Request Number and Username
        /// </summary>
        /// <param name="RequestNumber">RequestNumber entered during image upload</param>
        /// <param name="UserName">UserName entered during image upload</param>
        /// <returns></returns>
        [HttpGet]
        public HttpResponseMessage Search(string RequestNumber, string UserName)
        {
            try
            {
                var imageResult = new List<Image>();
                var imageSearchResponse = new List<ImageSearchResponse>();

                using (var surityDBContext = new SurityDBContext())
                {
                    imageResult = _imageUploadSessionRepository.SearchImages(RequestNumber, UserName);

                    var digitalCertificate = surityDBContext.DigitalCertificate.FirstOrDefault();

                    imageResult.ForEach(x => imageSearchResponse.Add(new ImageSearchResponse
                    {
                        ImageName = x.FileName,
                        ImageData = ImageCryptographyManager.GetImage(Path.Combine(FilePaths.XMLBasePath, x.SignedImageFileName), digitalCertificate.PublicKey)
                    }));

                    //File.WriteAllBytes(HttpContext.Current.Server.MapPath("~/") + "\\" + "Test_Generated.png", imageSearchResponse.FirstOrDefault().ImageData);
                }

                return new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(Newtonsoft.Json.JsonConvert.SerializeObject(imageSearchResponse))
                };
            }
            catch
            {
                // If an uncaught exception occurs, return an error response
                // with status code 500 (Internal Server Error)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, LanguageConstants.InternalServerError);
            }
                
        }

        [HttpPost]
        public HttpResponseMessage Upload(ImageUploadRequest imageUploadRequest)
        {
            try 
            {
                if (ModelState.IsValid)
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

                    var digitalCertificate = _digitalCertificateRepository.Query().FirstOrDefault();

                    ImageCryptographyManager.SignImage(
                        imageUploadRequest.ImageData,
                        //File.ReadAllBytes(HttpContext.Current.Server.MapPath("~/") + "\\" + "Test.png"),
                        digitalCertificate.PrivateKey,
                        Path.Combine(FilePaths.XMLBasePath, xmlDocumentName)
                    );

                    _imageUploadSessionRepository.Add(imageUploadSession);

                    return new HttpResponseMessage
                    {
                        StatusCode = HttpStatusCode.OK,
                        Content = new StringContent(LanguageConstants.ImageSuccessfullyUploaded)
                    };
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch
            {
                // If an uncaught exception occurs, return an error response
                // with status code 500 (Internal Server Error)
                return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, LanguageConstants.InternalServerError);
            }
        }
    }
}
