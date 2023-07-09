using System;
using System.Reflection;

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



    public static string FilterRoutes(this string path)
    {
        string response = "";
        string result = "";
        //var APIFolder = Environment.CurrentDirectory + "\\" + "API";
        var APIFolder = Path.Combine(Environment.CurrentDirectory, "API");
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


                if (reflected_method != null)
                {
                    if (reflected_method.RouteAPI == path)
                    {

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
        }
        if (string.IsNullOrEmpty(response))
        {
            result = EnvironmentExtensions.GetHTML("html/error.html");
            response = result;
        }
        return response;
    }
}