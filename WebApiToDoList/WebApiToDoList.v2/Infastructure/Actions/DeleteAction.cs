using WebApiToDoList.v2.Infastructure.Repository;
using WebApiToDoList.v2.Infastructure.Repository.DTO;
using WebApiToDoList.v2.Infastructure.Services;

namespace WebApiToDoList.v2.Infastructure.Actions {
    public class DeleteAction : Action {
        private const string DeleteUrl = "ToDos/{0}";
        private readonly Item item;

        public DeleteAction(Item item) {
            this.item = item;
        }

        public override void Do() {
            var remoteId = LocalRepository.Instance.Get(item.ToDoId, true)?.RemoteId;
            if (remoteId != null) {
                LocalRepository.Instance.Delete(item.ToDoId);
                HttpClient.DeleteAsync(string.Format(ServiceApiUrl + DeleteUrl, remoteId)).Result.EnsureSuccessStatusCode();
            }
        }
    }
}