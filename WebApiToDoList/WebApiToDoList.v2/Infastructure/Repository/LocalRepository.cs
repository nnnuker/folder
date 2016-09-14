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
            LocalAdd(item);
            var remoteIds = (from it in Items where it.RemoteId.HasValue select it.RemoteId.Value).ToList();
            AddWork(new AddAction(item, remoteIds));
        }
         public void LocalAdd(Item item) {
            item.ToDoId = ++id;
            Items.Add(item);
        }
        //-//-
        public void Delete(int id) {
            var item = Get(id);
            LocalDelete(id);
            AddWork(new DeleteAction(item));
        }

        public void LocalDelete(int id) {
            Items.RemoveAll(item => item.ToDoId == id);
        }

        public void LocalUpdate(Item item) {
            var removeId = item.RemoteId;
            foreach (var it in Items.Where(it => it.ToDoId == item.ToDoId)) {
                it.Name = item.Name;
                it.RemoteId = item.RemoteId;
                it.UserId = item.UserId;
                it.IsCompleted = item.IsCompleted;
            }
        }

        //-//-
        public void Update(Item item) {
            LocalUpdate(item);
            var currItem = Items.Find(it => it.ToDoId == item.ToDoId);
            AddWork(new UpdateAction(currItem));
        }
        public IList<Item> GetAll() {
            return Items;
        }

        public Item Get(int id) {
            return Items.Find(item => item.ToDoId == id);
        }
    }
}