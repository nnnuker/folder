using System.Collections.Generic;
using System.Net.Http;
using Newtonsoft.Json;
using WebApiToDoList.v2.Infastructure.Repository;
using WebApiToDoList.v2.Infastructure.Repository.DTO;

namespace WebApiToDoList.v2.Infastructure.Actions {
    public class AddAction : Action {
        private const string CreateUrl = "ToDos";
        private const string GetAllUrl = "ToDos?userId={0}";
        private readonly Item item;
        private readonly List<int> removeIds;

        public AddAction(Item item, List<int> removeIds) {
            this.item = item;
            this.removeIds = removeIds;
        }

        public override void Do() {
            HttpClient.PostAsJsonAsync(ServiceApiUrl + CreateUrl, item)
                            .Result.EnsureSuccessStatusCode();
            var dataAsString = HttpClient.GetStringAsync(string.Format(ServiceApiUrl + GetAllUrl, item.UserId)).Result;
            var remoteData = JsonConvert.DeserializeObject<IList<Item>>(dataAsString);
            int? itemId = null;
            if (removeIds.Count == 0) {
                itemId = remoteData[0].ToDoId;
            } else {
                foreach (var it in remoteData) {
                    if (!removeIds.Contains(it.ToDoId)) continue;
                    itemId = it.ToDoId;
                    break;
                }
            }
            item.RemoteId = itemId;
            LocalRepository.Instance.LocalUpdate(item);
        }
    }

    
}