using WebApiToDoList.v2.Infastructure.Repository.DTO;
using WebApiToDoList.v2.Models;

namespace WebApiToDoList.v2.Infastructure.Mappers {
    public static class ItemMapper {
        public static Item ToItem(this ToDoItemViewModel model) {
            if (model == null) return null;
            return new Item {
                ToDoId = model.ToDoId,
                Name = model.Name,
                UserId = model.UserId,
                IsCompleted = model.IsCompleted,
                RemoteId = null,
                IsDeleted = false
            };
        }
        public static ToDoItemViewModel ToViewModel(this Item item) {
            if(item == null)
                return null;
            return new ToDoItemViewModel {
                ToDoId = item.ToDoId,
                Name = item.Name,
                UserId = item.UserId,
                IsCompleted = item.IsCompleted
            };
        }
    }
}