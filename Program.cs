using System;
using System.Net;
using System.Net.Sockets;
using System.Text;

class Program
{
    static void Main()
    {
        IPAddress ipAddress;
        int port = 8080;
        string environment = Environment.GetEnvironmentVariable("ENVIRONMENT");

        if (EnvironmentExtensions.IsProductionEnvironment(environment))
        {
           ipAddress = IPAddress.Parse("169.254.129.3");
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
        Console.WriteLine("Request: \n" + request);
        
        string response = EnvironmentExtensions.GetHTML("html/index.html");

        byte[] responseBytes = Encoding.ASCII.GetBytes(response);
        stream.Write(responseBytes, 0, responseBytes.Length);

        client.Close();
    }
}
