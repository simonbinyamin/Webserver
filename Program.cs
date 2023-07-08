using System;
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


            string response = "";


            var APIFolder = Environment.CurrentDirectory + "\\" + "API";

            string[] apiFiles = Directory.GetFiles(APIFolder, "*.cs");


            foreach (string filePath in apiFiles)
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                Type apiType = Type.GetType("Webserver.API." + fileName);


                MethodInfo[] methods = apiType.GetMethods();

                foreach (MethodInfo method in methods)
                {

                    var reflected_method = (API_PathAttribute)Attribute
                                            .GetCustomAttribute(apiType
                                            .GetMethod(method.Name),
                                            typeof(API_PathAttribute));

                    if (reflected_method != null && reflected_method.RouteAPI == path)
                    {
                        string result = "";

                        MethodInfo methodtoexecute = apiType.GetMethod(method.Name);
                        if (methodtoexecute != null)
                        {
                            object myClassInstance = Activator.CreateInstance(apiType);
                            result = methodtoexecute.Invoke(myClassInstance, null).ToString();
                        }

                        response = result;

                    }

                }


            }





            byte[] responseBytes = Encoding.ASCII.GetBytes(response);
            stream.Write(responseBytes, 0, responseBytes.Length);

        }



        client.Close();
    }
}
