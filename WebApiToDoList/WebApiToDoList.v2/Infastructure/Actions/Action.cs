using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;

namespace WebApiToDoList.v2.Infastructure.Actions {
    public abstract class Action {
        protected readonly string ServiceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];
        protected readonly HttpClient HttpClient;

        protected Action() {
            HttpClient = new HttpClient();
            HttpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        public abstract void Do();
    }
}