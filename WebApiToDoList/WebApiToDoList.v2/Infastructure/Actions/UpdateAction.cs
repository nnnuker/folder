﻿using System.Net.Http;
using WebApiToDoList.v2.Infastructure.Mappers;
using WebApiToDoList.v2.Infastructure.Repository.DTO;
using WebApiToDoList.v2.Infastructure.Services;

namespace WebApiToDoList.v2.Infastructure.Actions {
    public class UpdateAction : Action {
        private const string UpdateUrl = "ToDos";
        private Item item;
        public UpdateAction(Item item) {
            this.item = item;
            
        }

        public override void Do() {
            var remoteId = Service.Instance.Get(item.ToDoId).RemoteId;
            if (remoteId != null) {
                item.ToDoId = remoteId.Value;
                HttpClient.PutAsJsonAsync(ServiceApiUrl + UpdateUrl, item.ToViewModel()).Result.EnsureSuccessStatusCode();
            }

        }
    }
}