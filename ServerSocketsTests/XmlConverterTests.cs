using Microsoft.VisualStudio.TestTools.UnitTesting;
using ServerSockets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ServerSockets.Tests
{
    [TestClass()]
    public class XmlConverterTests
    {
        string basePath = AppDomain.CurrentDomain.BaseDirectory;
   
        [TestMethod()]
        public void TestXmlReadRequest()
        {
            
            TextReader tr = new StreamReader(basePath + "/" + "RequestFile.xml");
            string myText = tr.ReadLine();
            
            Assert.AreEqual("<ConvertRequest>", myText);
        }

        [TestMethod()]
        public void TestXmlReadResponse()
        {
            TextReader tr = new StreamReader(basePath + "/" + "ResponseFile.xml");
            string myText = tr.ReadLine();
            Console.WriteLine(basePath);
            Assert.AreEqual("<ConvertResponse>", myText);
        }

        [TestMethod()]
        public void TestXmlErrorMessage()
        {
            var xmlText = XmlConverter.GenerarPaqueteXmlConvertResponseError("ERROR: Divisa no reconocida MXN");
            Assert.AreEqual("<?xml version=\"1.0\" encoding=\"UTF-8\"?><ConvertResponse>ERROR: Divisa no reconocida MXN</ConvertResponse>", xmlText);
        }

        [TestMethod()]
        public void ProcesarXmlConvertRequestTest()
        {
            TextReader tr = new StreamReader(basePath + "/" + "RequestFile.xml");
            string myText = tr.ReadToEnd();
            string operacion = XmlConverter.ProcesarXmlConvertRequest(myText, out decimal resultado);

            Assert.AreEqual(10, resultado);
            Assert.AreEqual("USD-EUR", operacion);

        }

        [TestMethod()]
        public void GenerarPaqueteXmlConvertResponseTest()
        {
            Assert.AreEqual("a", "a");
        }

        [TestMethod()]
        public void GenerarPaqueteXmlConvertResponseErrorTest()
        {
            Assert.AreEqual("a","a");
        }
    }
}