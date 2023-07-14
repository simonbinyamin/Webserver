

using Webserver.Interfaces;

namespace Webserver.API
{
   
    public class MarketAPI : IAuthuser
    {
        public MarketAPI() {

            
        }

        [API_Auth("/MarketAPI", "Jwt")]
        [API_Path("/MarketAPI")]
        public static string ProcessRequest()
        {

            string responseContent = EnvironmentExtensions.GetHTML("html/index.html");
            return responseContent;


        }

        public string Unauthorized()
        {
            return EnvironmentExtensions.GetHTML("html/unauthorized.html");
        }
    }
}
