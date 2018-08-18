using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace FMFunctions
{
    public static class Function1
    {
        [FunctionName("FMFunction")]
        public static async Task<HttpResponseMessage> Run([HttpTrigger(AuthorizationLevel.Function, "get", "post", Route = null)]HttpRequestMessage req, TraceWriter log)
        {
            log.Info("C# HTTP trigger function processed a request.");

            // parse query parameter
            string name = req.GetQueryNameValuePairs()
                .FirstOrDefault(q => string.Compare(q.Key, "name", true) == 0)
                .Value;

            int userId = 0;
            if (name == null)
            {
                // Get request body
                dynamic data = await req.Content.ReadAsAsync<FunctionParams>();
                name = data?.CustomerCode;
                userId = data?.UserId;
            }
            using (Initialise init = new Initialise("Data source=DESKTOP-G220MHL\\SQLEXPRESS;Initial Catalog=Common; user ID=sa;Password=sq-i00842r$;pooling='true';Max Pool Size=1000000"))
            {

            }

                return name == null
                    ? req.CreateResponse(HttpStatusCode.BadRequest, "Please pass a name on the query string or in the request body")
                    : req.CreateResponse(HttpStatusCode.OK, "Hello " + name + " user " + userId.ToString());
        }
    }
}
