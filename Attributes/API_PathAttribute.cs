[AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
public class API_PathAttribute : Attribute
{

    public string RouteAPI { get; }

    public API_PathAttribute(string parameter)
    {
        RouteAPI = parameter;
    }



}