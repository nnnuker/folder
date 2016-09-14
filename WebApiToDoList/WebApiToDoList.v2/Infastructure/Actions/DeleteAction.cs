namespace WebApiToDoList.v2.Infastructure.Actions {
    public class DeleteAction : Action {
        private const string DeleteUrl = "ToDos/{0}";
        private readonly int? id;
        public DeleteAction(int? id) {
            this.id = id;

        }

        public override void Do() {
            HttpClient.DeleteAsync(string.Format(ServiceApiUrl + DeleteUrl, id)).Result.EnsureSuccessStatusCode();
        }
    }
}