using WebApiToDoList.v2.Infastructure.Repository;
using WebApiToDoList.v2.Infastructure.Repository.DTO;

namespace WebApiToDoList.v2.Infastructure.Actions {
    public class DeleteAction : Action {
        private const string DeleteUrl = "ToDos/{0}";
        private readonly Item item;
        public DeleteAction(Item item) {
            this.item = item;

        }

        public override void Do() {
            var remoteId = LocalRepository.Instance.Get(item.ToDoId)?.RemoteId;
            if(remoteId != null) {
                HttpClient.DeleteAsync(string.Format(ServiceApiUrl + DeleteUrl, remoteId)).Result.EnsureSuccessStatusCode();
            }
        }
    }
}