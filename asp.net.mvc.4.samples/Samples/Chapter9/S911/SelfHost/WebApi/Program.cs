using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;

namespace WebApi
{
class Program
{
    static void Main(string[] args)
    {
        HttpSelfHostConfiguration configuration = new HttpSelfHostConfiguration("http://localhost:3721");
        configuration.Routes.MapHttpRoute("API Default", "api/{controller}/{id}", new { id = RouteParameter.Optional });
        using (HttpSelfHostServer server = new HttpSelfHostServer(configuration))
        {
            server.OpenAsync().Wait();
            Console.WriteLine("按任意键关闭Web API");
            Console.ReadLine();
        }
    }
}
}
