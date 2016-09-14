using System.Collections.Generic;
using System.Linq;
using WebApiToDoList.v2.Infastructure.Actions;
using WebApiToDoList.v2.Infastructure.Repository;
using WebApiToDoList.v2.Infastructure.Repository.DTO;

namespace WebApiToDoList.v2.Infastructure.Services {
    public class Service : IService {
        private readonly IRepository repository;
        public static Service Instance { get; }
      private Service () {
            repository = LocalRepository.Instance;
        }

        static Service () {
            Instance = new Service();
        }
        
        public void Add (Item item) {
            repository.Add(item);
            var items = repository.GetAll();
            IList<int> remoteIds = (from it in items where it.RemoteId.HasValue select it.RemoteId.Value).ToList();
            Worker.Worker.AddWork(new AddAction(item, remoteIds));
        }
        
        public void Delete (int id) {
            repository.LocalDelete(id);
            var item = repository.Get(id, true);
            Worker.Worker.AddWork(new DeleteAction(item));
        }

        public void Update (Item item) {
            LocalUpdate(item);
            var currItem = repository.Get(item.ToDoId, true);
            Worker.Worker.AddWork(new UpdateAction(currItem));
        }

        public void LocalUpdate(Item item) {
            repository.Update(item);
        }

        public IList<Item> GetAll () {
            return repository.GetAll();
        }

        public Item Get (int id) {
            return repository.Get(id, false);
        }
    }
}