using System.Net.Http;
using WebApiToDoList.v2.Infastructure.Repository.DTO;

namespace WebApiToDoList.v2.Infastructure.Actions {
    public class UpdateAction : Action {
        private const string UpdateUrl = "ToDos";
        private Item item;
        public UpdateAction(Item item) {
            this.item = item;
            item.RemoteId = item.RemoteId;
        }

        public override void Do() {
            HttpClient.PutAsJsonAsync(ServiceApiUrl + UpdateUrl, item)
                      .Result.EnsureSuccessStatusCode();
        }
    }
}