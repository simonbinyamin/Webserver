using System.Web.Http;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class CustomAuthorizeAttribute : AuthorizeAttribute
{

    public string RouteAPI { get; }

    public CustomAuthorizeAttribute(string parameter)
    {
        RouteAPI = parameter;
    }



}