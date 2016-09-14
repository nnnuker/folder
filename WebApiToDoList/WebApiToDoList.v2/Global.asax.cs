using System.Threading;
using System.Web.Http;
using WebApiToDoList.v2.Infastructure.Worker;

namespace WebApiToDoList.v2 {
    public class WebApiApplication : System.Web.HttpApplication {
        //TODO: think about this
        protected void Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            var th = new Thread(Worker.Instance.Run);
            th.Start();
        }
        protected void Application_End() {
            var ydsd = "";
        }
    }
}
