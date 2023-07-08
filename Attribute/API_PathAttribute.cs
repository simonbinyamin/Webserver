using System.Web.Http;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class API_PathAttribute : AuthorizeAttribute
{

    public string RouteAPI { get; }

    public API_PathAttribute(string parameter)
    {
        RouteAPI = parameter;
    }



}