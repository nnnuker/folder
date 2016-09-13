
namespace WebApiToDoList.Infastructure.Repository.DTO {
    public class Item {
        public int Id { get; set; }
        public int UserId { get; set; }
        public bool IsCompleted { get; set; }
        public string Name { get; set; }
        public int? RemoteId { get; set; }
    }
}