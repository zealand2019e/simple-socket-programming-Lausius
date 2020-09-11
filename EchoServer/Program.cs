using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EchoServer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Server.Start();

        }
    }

    public class Server
    {
        public static void Start()
        {
            int clientsConnected = 0;
            TcpListener serverSocket = new TcpListener(IPAddress.Loopback, 7);
            serverSocket.Start();
            while (true)
            {
                TcpClient connectionSocket = serverSocket.AcceptTcpClient();
                Console.WriteLine("Server activated");
                clientsConnected++;
                Console.WriteLine("Client " + clientsConnected + " has connected to the server.");
                Task.Run(() => DoClient(connectionSocket));
            }

            //ns.Close();
            //Console.WriteLine("net stream closed");
            //connectionSocket.Close();
            //Console.WriteLine("connection socket closed");
            //serverSocket.Stop();
            //Console.WriteLine("server stopped");
        }

        public static void DoClient(TcpClient socket)
        {
            using (socket)
            {
                while (true)
                {
                    Stream ns = socket.GetStream();
                    StreamReader sr = new StreamReader(ns);
                    StreamWriter sw = new StreamWriter(ns);
                    sw.AutoFlush = true;

                    var message = sr.ReadLine();
                    var words = message.Split(" ");
                    int wordNumber = words.Length;
                    Console.WriteLine("received message: " + message + " has " + wordNumber + " words");
                    if (message != null)
                    {
                        sw.WriteLine(message.ToUpper());
                    }
                }

            }
        }
    }

}
