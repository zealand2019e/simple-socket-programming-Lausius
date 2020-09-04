using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

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
            TcpListener serverSocket = new TcpListener(IPAddress.Loopback, 7);

            TcpClient connectionSocket = serverSocket.AcceptTcpClient();
            Console.WriteLine("Server activated");

            Stream ns = connectionSocket.GetStream();
            StreamReader sr = new StreamReader(ns);
            StreamWriter sw = new StreamWriter(ns);
            sw.AutoFlush = true;

            string message = sr.ReadLine();
            Console.WriteLine("received message: " + message);
            if (message != null)
            {
                sw.WriteLine(message.ToUpper());
            }

            ns.Close();
            Console.WriteLine("net stream closed");
            connectionSocket.Close();
            Console.WriteLine("connection socket closed");
            serverSocket.Stop();
            Console.WriteLine("server stopped");
        }
    }

}
