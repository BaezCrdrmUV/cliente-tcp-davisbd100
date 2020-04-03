using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

namespace SocketCom
{
    public class TCPCliente
    {
        String ClientName { get; set; }
        TcpClient Client { get; set; }

        public TCPCliente(String name, String server, int port)
        {
            ClientName = name;
            Client = new TcpClient(server, port);
        }

        public void SendMessage()
        {
            try
            {
                while(true)
                {
                    NetworkStream stream = Client.GetStream(); 
                    Console.WriteLine("Ingrese un mensaje para el servidor");
                    String message = Console.ReadLine();
                    Byte[] data = System.Text.Encoding.ASCII.GetBytes(message);  
                    Console.WriteLine("Enviando mensaje...");
                    stream.Write(data, 0, data.Length);
                    Console.WriteLine("Mensaje enviado");
                    if (message == "bye")
                    {
                        Console.WriteLine("Cerrando conexion");
                        break;
                    }
                    Console.WriteLine("Esperando respuesta...");
                    data = new byte[256];
                    String response = String.Empty;
                    Int32 bytes = stream.Read(data, 0, data.Length);
                    response = System.Text.Encoding.ASCII.GetString(data, 0, bytes);
                    Console.WriteLine("Respuesta: " + response);
                }
            }catch (SocketException)
            {
                Console.WriteLine("Error al conectar al servidor");
            }
        }
    }
}