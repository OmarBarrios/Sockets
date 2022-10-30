using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace ServerSockets
{
    public class CurrencyServer
    {
        public void Arrancar() 
        {
            try
            {
                // Hacemos que el TcpListener escuche en host:port.
                var host = Properties.Settings.Default.host;
                var port = Properties.Settings.Default.port;

                // Console.WriteLine("host: " + object.Equals(host, null) + " => " + host);
                // Console.WriteLine("port: " + object.Equals(port, null) + " => " + port);

                IPAddress localAddr = IPAddress.Parse(host);
                TcpListener server = new TcpListener(localAddr, Int32.Parse(port));
                server.Start();

                Byte[] bytes = new Byte[256];
                String data = null;
                while (true)
                {
                    // Se bloquea aceptando una petición de un TcpClient.
                    TcpClient client = server.AcceptTcpClient();
                    data = null;
                    NetworkStream stream = client.GetStream();
                    Int32 i = stream.Read(bytes, 0, bytes.Length);
                    data = System.Text.Encoding.ASCII.GetString(bytes, 0, i).Trim();

                    
                    decimal numeroAConvertir = 9;

                    var tipoConversion = XmlConverter.ProcesarXmlConvertRequest(data, out numeroAConvertir);

                    if (tipoConversion == "EUR-USD")
                        //Hacer conversión
                        data = XmlConverter.GenerarPaqueteXmlConvertResponse("1.0", "3");
                    else if (tipoConversion == "USD-EUR")
                        //Hacer conversión
                        data = XmlConverter.GenerarPaqueteXmlConvertResponse("9", "1.0");
                    else
                        data = XmlConverter.GenerarPaqueteXmlConvertResponseError("ERROR: Divisa no reconocida " + tipoConversion);

                    Byte[] msg = System.Text.Encoding.ASCII.GetBytes(data);

                    Console.Write("====> : " + data);

                    stream.Write(msg, 0, msg.Length);
                    client.Close();
                }
            }
            catch (SocketException e)
            {
                Console.WriteLine("SocketException: {0}", e);
            }
        }
    }
}
