using SeleniumFramework.Utilities.hooks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace SeleniumFramework.Utilities.XmlUtil
{
    class XMLTestDataReader
    {

        public static string xmlName = string.Empty;
        /*
         * Reads the data from the XML file based of the mentioned PBI ID
         * @parameters : tesCaseID =  PBI_ID, tagName = node/variable name
         */
        public string ReadData(string xmlName, string tagName)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string path = projectPath.Replace(@"\bin\Debug\netcoreapp2.1\", "") + @"\TestData\" + xmlName + ".xml";
            
            XDocument xml = XDocument.Load(path);
            string text = string.Empty;
            var nodes = (from n in xml.Descendants("TestData").
             Where(r => r.Parent.Attribute("PBI_ID").Value == GlobalVariables.testCaseID.Replace("TC_",""))
                         select new
                         {
                             testData = (string)n.Element(tagName).Value,
                         });
            foreach (var n in nodes)
            {
                text = n.testData;
            }
            return text;
        }
    }
}
