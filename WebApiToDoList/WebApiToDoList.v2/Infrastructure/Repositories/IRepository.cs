using System.Collections.Generic;
using WebApiToDoList.Models;

namespace WebApiToDoList.Infrastructure.Repositories
{
    public interface IRepository<T>
    {
        void Add(T model);
        void Delete(int id);
        void Update(T model);
        IList<T> GetAll();
        T GetById(int id);
    }
}