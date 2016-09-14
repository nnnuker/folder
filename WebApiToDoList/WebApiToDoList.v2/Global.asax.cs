using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiToDoList.v2.Infastructure.Worker;
    
namespace WebApiToDoList.v2 {
    public class WebApiApplication : System.Web.HttpApplication {
        //TODO: think about this
        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var th = new Thread(new Worker().Run);
            th.Start();
        }
    }
}
