using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SurityCryptography
{
    public static class DigitalCertificateManager
    {
        public static void SignImage(byte[] imageBuffer, byte[] certificateBuffer, string xmlFilePath) 
        {
            var certificate = new X509Certificate2(certificateBuffer);

            var xmlDocumentBuffer = convertImageToXML(imageBuffer);

            signXMLDocument(xmlDocumentBuffer, certificate, xmlFilePath);
        }

        public static bool VerifyImage(string xmlFilePath, byte[] certificateBuffer)
        {
            var certificate = new X509Certificate2(certificateBuffer);

            return verifyXMLDocument(xmlFilePath, certificate);
        }

        public static byte[] GetImage(string xmlFilePath, byte[] certificateBuffer) 
        {
            var certificate = new X509Certificate2(certificateBuffer);

            var unsignedXMLDocument = unsignXMLDocument(xmlFilePath, certificate);

            return convertXMLToImage(unsignedXMLDocument);
        }




        // ============================== Helper Methods ==============================

        private static byte[] convertImageToXML(byte[] imageBuffer)
        {
            XmlSerializer x = new XmlSerializer(typeof(byte[]));

            using (MemoryStream myFileStream = new MemoryStream())
            {
                x.Serialize(myFileStream, imageBuffer);

                return myFileStream.ToArray();
            }
        }

        private static byte[] convertXMLToImage(XmlDocument xmlDocument)
        {
            StringWriter stringWriter = new StringWriter();
            XmlTextWriter xmlTextWriter = new XmlTextWriter(stringWriter);

            // Save Xml Document to Text Writter.
            xmlDocument.WriteTo(xmlTextWriter);
            UTF8Encoding encoding = new UTF8Encoding();

            // Convert Xml Document To Byte Array.
            byte[] xmlDocumentBuffer = encoding.GetBytes(stringWriter.ToString());

            XmlSerializer mySerializer = new XmlSerializer(typeof(byte[]));

            using (MemoryStream myFileStream = new MemoryStream(xmlDocumentBuffer))
            {
                return (byte[]) mySerializer.Deserialize(myFileStream);
            }
        }

        private static void signXMLDocument(byte[] xmlDocumentBuffer, X509Certificate2 certificate, string signedXMLPath)
        {
            XmlDocument xmlDocument = new XmlDocument();
            string xml = Encoding.UTF8.GetString(xmlDocumentBuffer);
            xmlDocument.LoadXml(xml);

            using (var rsaKey = certificate.PrivateKey)
            {
                var signedXml = new SignedXml(xmlDocument);
                signedXml.SigningKey = rsaKey;

                var reference = new Reference();
                reference.Uri = "";

                var env = new XmlDsigEnvelopedSignatureTransform();
                reference.AddTransform(env);

                signedXml.AddReference(reference);

                signedXml.ComputeSignature();

                var xmlDigitalSignature = signedXml.GetXml();

                xmlDocument.DocumentElement.AppendChild(xmlDocument.ImportNode(xmlDigitalSignature, true));

                xmlDocument.Save(signedXMLPath);
            }
        }

        private static XmlDocument unsignXMLDocument(string xmlFilePath, X509Certificate2 certificate) 
        {
            var xmlDocument = readXMLDocumentFromPath(xmlFilePath);

            var nodeList = xmlDocument.GetElementsByTagName("Signature");

            xmlDocument.DocumentElement.RemoveChild(nodeList[0]);

            return xmlDocument;
        }

        private static bool verifyXMLDocument(string xmlFilePath, X509Certificate2 certificate)
        {
            var xmlDocument = readXMLDocumentFromPath(xmlFilePath);

            var signedXml = new SignedXml(xmlDocument);

            var nodeList = xmlDocument.GetElementsByTagName("Signature");

            signedXml.LoadXml((XmlElement)nodeList[0]);

            using (var rsaKey = certificate.PublicKey.Key)
            {
                return signedXml.CheckSignature(rsaKey);
            }
        }

        private static XmlDocument readXMLDocumentFromPath(string xmlFilePath)
        {
            // Read XML Document
            var xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.Load(xmlFilePath);

            return xmlDocument;
        }
    }
}
