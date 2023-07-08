

namespace Webserver.API
{
   
    public class StockAPI
    {
        public StockAPI() {

            
        }

        [API_Path("/StockAPI0")]
        public static string ProcessRequest()
        {

            string responseContent = EnvironmentExtensions.GetHTML("html/index.html");
            return responseContent;


        }



        [API_Path("/hej")]
        public static string Hej()
        {

            string responseContent = EnvironmentExtensions.GetHTML("html/hej.html");
            return responseContent;


        }
    }
}
