using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace DigitalCertificateKeysToBase64
{
    /*
     *  This is a utility program created to extract Public keys and Private keys from the Digital Certificate as Base 64 strings
     *  The base 64 string based Certificate keys will be used as initial Database Meta Data for DigitalCertificates.
     */
    class Program
    {
        static void Main(string[] args)
        {
            var privateKeyCert = getPrivateKeyCert();
            var certStringBase64 = Convert.ToBase64String(privateKeyCert.Export(X509ContentType.Pfx));
            File.WriteAllText("PrivateKey.txt", certStringBase64);

            var publicKeyCert = getPublicKeyCert();
            var publicKeyCertBase64 = Convert.ToBase64String(publicKeyCert.Export(X509ContentType.Pfx));
            File.WriteAllText("PublicKey.txt", publicKeyCertBase64);
        }

        public static X509Certificate2 getPrivateKeyCert()
        {
            var certyBytes = File.ReadAllBytes(ConfigurationManager.AppSettings["PrivateKeyCert"]);
            X509Certificate2 cert = new X509Certificate2(certyBytes, ConfigurationManager.AppSettings["PrivateKeyCertPassword"], X509KeyStorageFlags.Exportable);
            return cert;
        }

        public static X509Certificate2 getPublicKeyCert()
        {
            var certyBytes = File.ReadAllBytes(ConfigurationManager.AppSettings["PublicKeyCert"]);
            X509Certificate2 cert = new X509Certificate2(certyBytes);
            return cert;
        }
    }
}
