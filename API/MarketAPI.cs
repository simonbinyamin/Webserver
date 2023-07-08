

namespace Webserver.API
{
   
    public class MarketAPI
    {
        public MarketAPI() {

            
        }
        [API_Path("/MarketAPI")]
        public static string ProcessRequest()
        {

            string responseContent = EnvironmentExtensions.GetHTML("html/index.html");
            return responseContent;


        }
    }
}
