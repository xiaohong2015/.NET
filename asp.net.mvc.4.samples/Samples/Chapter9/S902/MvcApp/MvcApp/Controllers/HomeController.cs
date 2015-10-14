using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        public void Index()
        {
            HttpConfiguration configuration = new HttpConfiguration();
            configuration.MessageHandlers.Add(new FooMessageHandler());
            configuration.MessageHandlers.Add(new BarMessageHandler());
            configuration.MessageHandlers.Add(new BazMessageHandler());

            HttpServer httpServer = new HttpServer(configuration);
            Response.Write("初始化前：" + GetPipeline(httpServer) + "<br/>");

            //以反射的方式执行SendAsync方法促使HttpMessageHandler管道的创建
            MethodInfo method = typeof(HttpMessageHandler).GetMethod("SendAsync",BindingFlags.Instance | BindingFlags.NonPublic);
            method.Invoke(httpServer, new object[]{new HttpRequestMessage(),  new CancellationToken()});
            Response.Write("初始化后：" + GetPipeline(httpServer) + "<br/>");
        }

        private string GetPipeline(HttpMessageHandler httpServer)
        {
            string pipeline = httpServer.GetType().Name;
            DelegatingHandler delegatingHandler = httpServer as DelegatingHandler;
            if (null != delegatingHandler && delegatingHandler.InnerHandler != null)
            {
                return pipeline + " => " + GetPipeline(delegatingHandler.InnerHandler);
            }
            else
            {
                return pipeline;
            }
        }
    }
}
