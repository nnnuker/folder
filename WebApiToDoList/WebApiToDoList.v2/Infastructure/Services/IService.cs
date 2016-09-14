using System.Collections;
using System.Collections.Generic;
using WebApiToDoList.v2.Infastructure.Repository.DTO;

namespace WebApiToDoList.v2.Infastructure.Services {
    public interface IService {
        void Add(Item item);
        void Delete(int id);
        void Update(Item item);
        void LocalUpdate(Item item);
        IList<Item> GetAll();
        Item Get(int id);

    }
}