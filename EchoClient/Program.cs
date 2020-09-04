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
            TcpClient clientSocket = new TcpClient("localhost", 7777);
            using (clientSocket)
            {
                Console.WriteLine("server found");
                Stream ns = clientSocket.GetStream();
                StreamReader sr = new StreamReader(ns);
                StreamWriter sw = new StreamWriter(ns);
                sw.AutoFlush = true;

                string message = Console.ReadLine();
                sw.WriteLine(message);
                string serverAnswer = sr.ReadLine();
            }
        }
    }
}