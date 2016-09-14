using System.Collections.Generic;
using System.Linq;
using WebApiToDoList.v2.Infastructure.Actions;
using WebApiToDoList.v2.Infastructure.Repository.DTO;
using static WebApiToDoList.v2.Infastructure.Worker.Worker;

namespace WebApiToDoList.v2.Infastructure.Repository {
    public class LocalRepository : IRepository {
        private int id;
        private List<Item> Items { get; set; }
        public static LocalRepository Instance { get; }

        private LocalRepository() {
            Items = new List<Item>();
        }

        static LocalRepository() {
            Instance = new LocalRepository();
        }

        //IList<Item> items;
        public void Add(Item item) {
            item.ToDoId = ++id;
            Items.Add(item);
            var remoteIds = (from it in Items where it.RemoteId.HasValue select it.RemoteId.Value).ToList();
            AddWork(new AddAction(item, remoteIds));
        }
        //-//-
        public void Delete(int id) {
            var removeId = Items.Find(item => item.ToDoId == id).RemoteId;
            Items.RemoveAll(item => item.ToDoId == id);
            AddWork(new DeleteAction(removeId));
        }

        public void LocalUpdate(Item item) {
            var currItem = Items.Find(it => it.ToDoId == item.ToDoId);
            currItem.Name = item.Name;
            currItem.IsCompleted = item.IsCompleted;
            currItem.UserId = item.UserId;
            currItem.RemoteId = item.RemoteId;
        }
        //-//-
        public void Update(Item item) {
            LocalUpdate(item);
            AddWork(new UpdateAction(item));
        }
        public IList<Item> GetAll() {
            return Items;
        }

        public Item Get(int id) {
            return Items.Find(item => item.ToDoId == id);
        }
    }
}