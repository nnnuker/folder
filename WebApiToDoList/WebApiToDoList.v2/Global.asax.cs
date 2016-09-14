using System.Threading.Tasks;
using System.Web.Http;
using WebApiToDoList.v2.Infastructure.Worker;
    
namespace WebApiToDoList.v2 {
    public class WebApiApplication : System.Web.HttpApplication {
        //TODO: think about this
        protected async Task Application_Start() {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            await Worker.Run();
        }
    }
}
