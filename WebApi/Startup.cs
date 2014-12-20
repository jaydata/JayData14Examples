using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using System.Web.Http;
using System.Web.Http.Batch;
[assembly: OwinStartup(typeof(WebApi.Startup))]

namespace WebApi
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            HttpConfiguration configuration = new HttpConfiguration();
            HttpServer server = new HttpServer(configuration);
            configuration.Routes.MapHttpBatchRoute(
                routeName: "batch",
                routeTemplate: "odata/$batch",
                batchHandler: new DefaultHttpBatchHandler(server));            
            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
            WebApiConfig.Register(configuration);
            app.UseWebApi(server);
            // For more information on how to configure your application, visit http://go.microsoft.com/fwlink/?LinkID=316888
        }
    }
}
