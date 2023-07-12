using System;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Reflection.Metadata;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Webserver.API;

class Program
{

    static void Main(string[] args)
    {
        IPAddress ipAddress;
        int port = 8080;
        string environment = Environment.GetEnvironmentVariable("ENVIRONMENT");

       
        if (EnvironmentExtensions.IsProductionEnvironment(environment))
        {
           ipAddress = IPAddress.Parse("169.254.129.2");
        }
        else
        {
            ipAddress = IPAddress.Parse("127.0.0.1");
        }


        TcpListener listener = new TcpListener(ipAddress, port);
        listener.Start();

        while (true)
        {
            TcpClient client = listener.AcceptTcpClient();
            ThreadPool.QueueUserWorkItem(HandleClientRequest, client);
        }
    }


    static void HandleClientRequest(object clientObj)
    {

        TcpClient client = (TcpClient)clientObj;
        byte[] buffer = new byte[1024];
        NetworkStream stream = client.GetStream();
        int bytesRead = stream.Read(buffer, 0, buffer.Length);
        string request = Encoding.ASCII.GetString(buffer, 0, bytesRead);



        if (!string.IsNullOrEmpty(request))
        {
            string[] requestLines = request.Split(new[] { "\r\n" }, StringSplitOptions.None);
            string[] requestTokens = requestLines[0].Split(' ');
            string path = requestTokens[1];
            byte[] responseBytes;


            responseBytes = Encoding.ASCII.GetBytes(EnvironmentExtensions.FilterRoutes(path));


            stream.Write(responseBytes, 0, responseBytes.Length);

        }



        client.Close();
    }
}
