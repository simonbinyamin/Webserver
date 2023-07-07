using System.Net.Sockets;
using System.Text;

namespace Webserver.API
{
   
    public class StockAPI
    {
        public StockAPI() {

            
        }

        [CustomAuthorize("/StockAPI")]
        public static string ProcessRequest()
        {

            string responseContent = EnvironmentExtensions.GetHTML("html/index.html");
            return responseContent;


        }
    }
}
