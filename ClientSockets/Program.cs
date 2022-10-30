using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ClientSockets
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string basePath = AppDomain.CurrentDomain.BaseDirectory;
            string host = Properties.Settings.Default.host;
            string port = Properties.Settings.Default.port;

            TcpClient tcpClient = new TcpClient(host, (int)long.Parse(port));
            NetworkStream networkStream = tcpClient.GetStream();

            TextReader tr = new StreamReader(basePath + "/" + "RequestFile.xml");
            string myText = tr.ReadToEnd();

            byte[] byteMensaje = Encoding.ASCII.GetBytes(myText);

            networkStream.Write(byteMensaje, 0, byteMensaje.Length);
            Console.ReadLine();


            byte[] bytesRespuesta = new byte[256];
            int numBytesRespuesta = networkStream.Read(bytesRespuesta, 0, bytesRespuesta.Length);

            string Respuesta = Encoding.ASCII.GetString(bytesRespuesta);
            Console.WriteLine("Respuesta recibida en el cliente: " + Respuesta);
            networkStream.Close();
            Console.ReadLine();


        }
    }
}
