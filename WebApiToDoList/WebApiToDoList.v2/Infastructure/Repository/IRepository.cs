using System.Collections.Generic;
using WebApiToDoList.v2.Infastructure.Repository.DTO;

namespace WebApiToDoList.v2.Infastructure.Repository {
    public interface IRepository {
        void Add(Item item);
        void LocalDelete(int id);
        void Delete(int id);
        void Update(Item item);
        IList<Item> GetAll();
        Item Get(int id, bool withDeleted);
    }
}