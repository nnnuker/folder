using System.Collections.Generic;
using WebApiToDoList.Infastructure.Repository.DTO;

namespace WebApiToDoList.Infastructure.Repository {
    public class LocalRepository : IRepository {
        //IList<Item> items;
        //TODO: add to items, then "Worker.AddWork(new CreateAction())"
        public void Add(Item item) {
            throw new System.NotImplementedException();
        }
        //-//-
        public void Delete(int id) {
            throw new System.NotImplementedException();
        }
        //-//-
        public void Update(Item item) {
            throw new System.NotImplementedException();
        }
        //here don't do nothing with Worker
        public IList<Item> GetAll() {
            throw new System.NotImplementedException();
        }

        public Item Get(int id) {
            throw new System.NotImplementedException();
        }
    }
}