using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using WebApiToDoList.Models;

namespace WebApiToDoList.Infrastructure.Repositories
{
    public class LocalRepository : IRepository<ToDoItemViewModel>
    {
        private static readonly IRepository<ToDoItemViewModel> repository = new LocalRepository();
        private static List<ToDoItemViewModel> items = new List<ToDoItemViewModel>();
        private readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();

        static LocalRepository()
        {
        }

        public static IRepository<ToDoItemViewModel> GetRepository => repository;

        private LocalRepository()
        {
        }

        public void Add(ToDoItemViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));

            items.Add(model);
        }

        public void Delete(int id)
        {
            items.Remove(items.Find(item => item.ToDoId == id));
        }

        public void Update(ToDoItemViewModel model)
        {
            if (model == null) throw new ArgumentNullException(nameof(model));
            lockSlim.EnterWriteLock();
            try
            {
                var item = items.Find(i => i.ToDoId == model.ToDoId);
                items.Remove(item);
                items.Add(model);
            }
            finally
            {
                lockSlim.ExitWriteLock();
            }
            
        }

        public IList<ToDoItemViewModel> GetAll()
        {
            lockSlim.EnterReadLock();
            try
            {
                return items;
            }
            finally
            {
                lockSlim.ExitReadLock();
            }
        }

        public ToDoItemViewModel GetById(int id)
        {
            lockSlim.EnterReadLock();
            try
            {
                return items.Find(i=>i.ToDoId == id);
            }
            finally
            {
                lockSlim.ExitReadLock();
            }
        }
    }

}