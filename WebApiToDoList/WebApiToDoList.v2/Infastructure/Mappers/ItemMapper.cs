
﻿using WebApiToDoList.v2.Infastructure.Repository.DTO;
using WebApiToDoList.v2.Models;

    public static class ItemMapper {
        public static Item ToItem(this ToDoItemViewModel model) {
            if (model == null) return null;
            return new Item {
                Id = model.ToDoId,
                Name = model.Name,
                UserId = model.UserId,
                IsCompleted = model.IsCompleted,
                RemoteId = null
            };
        }
        public static ToDoItemViewModel ToViewModel(this Item item) {
            if(item == null)
                return null;
            return new ToDoItemViewModel {
                ToDoId = item.Id,
                Name = item.Name,
                UserId = item.UserId,
                IsCompleted = item.IsCompleted
            };
        }
    }
}