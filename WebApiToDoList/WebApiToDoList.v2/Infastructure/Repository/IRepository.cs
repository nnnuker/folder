using System.Collections.Generic;
using WebApiToDoList.Infastructure.Repository.DTO;

namespace WebApiToDoList.Infastructure.Repository {
    public interface IRepository {
        void Add(Item item);
        void Delete(int id);
        void Update(Item item);
        IList<Item> GetAll();
        Item Get(int id);
    }
}