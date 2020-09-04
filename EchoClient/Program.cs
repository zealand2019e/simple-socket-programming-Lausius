﻿using System;
using System.IO;
using System.Net.Sockets;

namespace EchoClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Client.Start();
        }
    }

    public class Client
    {
        public static void Start()
        {
            TcpClient clientSocket = new TcpClient("192.168.24.174", 7);
            using (clientSocket)
            {
                Console.WriteLine("server found");
                Stream ns = clientSocket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true;
                while (true)
                {
                    string message = Console.ReadLine();
                    sw.WriteLine(message);
                    string serverAnswer = sr.ReadLine();
                    Console.WriteLine(serverAnswer);
                }
            }
        }
    }
}
