﻿using System;
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
    public static class ImageCryptographyManager
    {
        public static void SignImage(byte[] imageBuffer, byte[] certificateBuffer, string xmlFilePath) 
        {
            var certificate = new X509Certificate2(certificateBuffer);

            var xmlDocumentBuffer = ConvertImageToXML(imageBuffer);

            SignXMLDocument(xmlDocumentBuffer, certificate, xmlFilePath);
        }

        public static bool VerifyImage(string xmlFilePath, byte[] certificateBuffer)
        {
            var certificate = new X509Certificate2(certificateBuffer);

            return VerifyXMLDocument(xmlFilePath, certificate);
        }

        public static byte[] GetImage(string xmlFilePath, byte[] certificateBuffer) 
        {
            var certificate = new X509Certificate2(certificateBuffer);

            var unsignedXMLDocument = UnsignXMLDocument(xmlFilePath, certificate);

            return ConvertXMLToImage(unsignedXMLDocument);
        }




        // ============================== Helper Methods ==============================

        private static byte[] ConvertImageToXML(byte[] imageBuffer)
        {
            XmlSerializer x = new XmlSerializer(typeof(byte[]));

            using (MemoryStream myFileStream = new MemoryStream())
            {
                x.Serialize(myFileStream, imageBuffer);

                return myFileStream.ToArray();
            }
        }

        private static byte[] ConvertXMLToImage(XmlDocument xmlDocument)
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

        private static void SignXMLDocument(byte[] xmlDocumentBuffer, X509Certificate2 certificate, string signedXMLPath)
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

        private static XmlDocument UnsignXMLDocument(string xmlFilePath, X509Certificate2 certificate) 
        {
            var xmlDocument = ReadXMLDocumentFromPath(xmlFilePath);

            var nodeList = xmlDocument.GetElementsByTagName("Signature");

            xmlDocument.DocumentElement.RemoveChild(nodeList[0]);

            return xmlDocument;
        }

        private static bool VerifyXMLDocument(string xmlFilePath, X509Certificate2 certificate)
        {
            var xmlDocument = ReadXMLDocumentFromPath(xmlFilePath);

            var signedXml = new SignedXml(xmlDocument);

            var nodeList = xmlDocument.GetElementsByTagName("Signature");

            signedXml.LoadXml((XmlElement)nodeList[0]);

            using (var rsaKey = certificate.PublicKey.Key)
            {
                return signedXml.CheckSignature(rsaKey);
            }
        }

        private static XmlDocument ReadXMLDocumentFromPath(string xmlFilePath)
        {
            // Read XML Document
            var xmlDocument = new XmlDocument();
            xmlDocument.PreserveWhitespace = true;
            xmlDocument.Load(xmlFilePath);

            return xmlDocument;
        }
    }
}
