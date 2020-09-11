using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace EchoServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Server server = new Server();
            server.Start();

        }
    }

    public class Server
    {
        int clientsConnected = 0;
        public void Start()
        {
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

        public void DoClient(TcpClient socket)
        {
            using (socket)
            {
                while (true)
                {
                    try
                    {
                        Stream ns = socket.GetStream();
                        StreamReader sr = new StreamReader(ns);
                        StreamWriter sw = new StreamWriter(ns);
                        sw.AutoFlush = true;
                        var message = sr.ReadLine();
                        var words = message.Split(" ");
                        int wordNumber = words.Length;
                        Console.WriteLine("received message: " + message + " has " + wordNumber + " words");
                        //Console.WriteLine(message);
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("Connection ended unexpectedly");
                        socket.Close();
                    }

                    if (socket.Connected == false)
                    {
                        Console.WriteLine("Client: " + clientsConnected + " has left the server.");
                        clientsConnected--;
                        socket.Dispose();
                        break;
                    }
                }

            }
        }
    }

}
