using System;
using System.Collections.Generic;
using System.Configuration;
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
            string host = ConfigurationManager.AppSettings["host"];
            string port = ConfigurationManager.AppSettings["port"];

            TcpClient tcpClient = new TcpClient(host, (int)long.Parse(port));

            NetworkStream networkStream = tcpClient.GetStream();

            byte[] byteMensaje = Encoding.ASCII.GetBytes("Mensaje entre sockets :)");

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
