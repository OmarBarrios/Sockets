using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace ServerSockets
{
    public class XmlConverter
    {

        public static string ProcesarXmlConvertRequest(string xmlData, out decimal resultado)
        {
            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xmlData);

            XmlNode root = doc.DocumentElement;

            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);
            nsmgr.AddNamespace("tst", "urn:data-schema");


            var fromUnits = root.SelectSingleNode("descendant::tst:from", nsmgr);
            var toUnits = root.SelectSingleNode("descendant::tst:to", nsmgr);
            var nodeUnits = root.SelectSingleNode("descendant::tst:units", nsmgr);
            
            Console.WriteLine("node:  " + nodeUnits.InnerText);

            resultado = Convert.ToDecimal(nodeUnits.InnerText);
            

            return String.Concat(fromUnits.InnerText.ToUpper().Trim(), "-", toUnits.InnerText.ToUpper().Trim());
        }

        public static string GenerarPaqueteXmlConvertResponse(string finalCurrency, string units)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode responseNode = doc.CreateElement("ConvertResponse");
            doc.AppendChild(responseNode);
            XmlNode unitsNode = doc.CreateElement("units");
            unitsNode.InnerText = units;
            responseNode.AppendChild(unitsNode);
            XmlNode toNode = doc.CreateElement("to");
            toNode.InnerText = finalCurrency;
            responseNode.AppendChild(toNode);
            return doc.InnerXml;
        }

        public static string GenerarPaqueteXmlConvertResponseError(string message)
        {
            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode responseNode = doc.CreateElement("ConvertResponse");
            responseNode.InnerText = message;
            doc.AppendChild(responseNode);
            return doc.InnerXml;
        }

    }
}
