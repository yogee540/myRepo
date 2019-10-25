using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SeleniumFramework.Utilities.apiUtil
{
    public class RSAEncrypt
    {
        private static RSACryptoServiceProvider csp = new RSACryptoServiceProvider(2048);
        private RSAParameters privatekey;
        private RSAParameters publickey;

        public  RSAEncrypt()
        {
            privatekey = csp.ExportParameters(true);
            publickey = csp.ExportParameters(false);
        }

        public string publickeystring()
        {
            var sw = new StringWriter();
            var xs = new XmlSerializer(typeof(RSAParameters));
            xs.Serialize(sw, publickey);
            return sw.ToString();
        }

        public string Encrypt(string text)
        {
            csp = new RSACryptoServiceProvider();
            csp.ImportParameters(publickey);
            var data = Encoding.Unicode.GetBytes(text);
            var cypher = csp.Encrypt(data, false);
            return Convert.ToBase64String(cypher);
        }

    }
}

