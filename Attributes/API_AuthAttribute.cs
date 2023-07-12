using Microsoft.AspNetCore.Authorization;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class API_AuthAttribute : AuthorizeAttribute
{

    public string AuthType { get; }
    public string RouteAPI { get; }

    public API_AuthAttribute(string routeAPI, string authType)
    {
        RouteAPI = routeAPI;
        AuthType = authType;
    }




}