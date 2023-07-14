

namespace Webserver.API
{
   
    public class StockAPI
    {
        public StockAPI() {

            
        }

        [API_Path("/")]
        public static string ProcessRequest()
        {

            string responseContent = EnvironmentExtensions.GetHTML("html/index.html");
            return responseContent;


        }



        [API_Path("/hej")]
        public static string Hej()
        {

            string responseContent = EnvironmentExtensions.GetJSON(new {name= "sadsadsa he" });
            return responseContent;


        }
    }
}
