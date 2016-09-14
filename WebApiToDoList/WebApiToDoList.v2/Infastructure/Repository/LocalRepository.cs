using System.Collections.Generic;
using System.Linq;
using WebApiToDoList.v2.Infastructure.Actions;
using WebApiToDoList.v2.Infastructure.Repository.DTO;
using static WebApiToDoList.v2.Infastructure.Worker.Worker;

namespace WebApiToDoList.v2.Infastructure.Repository {
    public class LocalRepository : IRepository {
        private static readonly List<Item> Items = new List<Item>();
        static LocalRepository() { }

        public static IRepository GetRepository { get; } = new LocalRepository();

        private LocalRepository() { }


        //IList<Item> items;
        //TODO: add to items, then "Worker.AddWork(new CreateAction())"
        public void Add(Item item) {
            Items.Add(item);
            AddWork(new AddAction());
        }
        //-//-
        public void Delete(int id) {
            Items.RemoveAll(item => item.Id == id);
            AddWork(new DeleteAction());
        }
        //-//-
        public void Update(Item item) {
            throw new System.NotImplementedException();
        }
        //here don't do nothing with Worker
        public IList<Item> GetAll() {
            return Items;
        }

        public Item Get(int id) {
            return Items.Find(item => item.Id == id);
        }
    }
}