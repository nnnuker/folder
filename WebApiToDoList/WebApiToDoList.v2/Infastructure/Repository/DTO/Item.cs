
namespace WebApiToDoList.v2.Infastructure.Repository.DTO {
    public class Item {
        public int ToDoId { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
        public int? RemoteId { get; set; }
    }
}