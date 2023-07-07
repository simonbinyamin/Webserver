using System;

public static class EnvironmentExtensions
{
    public static bool IsProductionEnvironment(this string environment)
    {
        if(String.IsNullOrEmpty(environment)){
            return false;
        }
        return environment == "PROD";
    }


    public static string GetHTML(this string filePath)
    {
        string response;
        if (File.Exists(filePath))
        {
            string htmlContent = File.ReadAllText(filePath);
            response = "HTTP/1.1 200 OK\r\n" +
                        "Content-Type: text/html\r\n" +
                        "\r\n" +
                        htmlContent;
        }
        else
        {
            response = "HTTP/1.1 404 Not Found\r\n" +
                        "Content-Type: text/plain\r\n" +
                        "\r\n" +
                        "404 - File Not Found";
        }

        return response;
    }




}