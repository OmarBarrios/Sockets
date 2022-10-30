using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;

namespace ServerSockets
{
    internal class Program
    {
        static void Main(string[] args)
        {

            CurrencyServer server = new CurrencyServer();

            server.Arrancar();

        }
        void firstServer()
        {
            IPAddress iPAddress = IPAddress.Parse("127.0.0.1");
            TcpListener tcpListener = new TcpListener(iPAddress, 13000);
            tcpListener.Start();
            byte[] buffer = new byte[256];
            String data = "";

            while (true)
            {
                TcpClient tcpClient = tcpListener.AcceptTcpClient();
                NetworkStream stream = tcpClient.GetStream();

                int indice = 0;

                while ((indice = stream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    data = System.Text.Encoding.ASCII.GetString(buffer, 0, indice);
                    Console.Write("Servidor recibe: " + data);

                    var respuesta = data.ToUpper();
                    byte[] bytesRespuesta = Encoding.ASCII.GetBytes(respuesta);
                    stream.Write(bytesRespuesta, 0, bytesRespuesta.Length);

                }
            }
        }
    }
}
