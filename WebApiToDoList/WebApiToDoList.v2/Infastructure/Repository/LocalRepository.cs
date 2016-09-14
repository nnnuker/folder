using System.Collections.Generic;
using System.Linq;
using System.Threading;
using WebApiToDoList.v2.Infastructure.Repository.DTO;

namespace WebApiToDoList.v2.Infastructure.Repository {
    public class LocalRepository : IRepository {
        private int id;
        private List<Item> Items { get; set; }
        public static LocalRepository Instance { get; }
        private static readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();

        private LocalRepository() {
            Items = new List<Item>();
        }

        static LocalRepository() {
            Instance = new LocalRepository();
        }

        public void Add(Item item) {
            lockSlim.EnterWriteLock();
            try {
                item.ToDoId = ++id;
                Items.Add(item);
            } finally {
                lockSlim.ExitWriteLock();
            }
        }

        public void LocalDelete(int id) {
            lockSlim.EnterWriteLock();
            try {
                foreach (var item in Items.Where(item => item.ToDoId == id)) {
                    item.IsDeleted = true;
                }
            } finally {
                lockSlim.ExitWriteLock();
            }
        }

        public
            void Delete(int id) {
            lockSlim.EnterWriteLock();
            try {
                Items.RemoveAll(item => item.ToDoId == id);
            } finally {
                lockSlim.ExitWriteLock();
            }
        }

        public void Update(Item item) {
            lockSlim.EnterWriteLock();
            try {
                foreach (var it in Items.Where(it => it.ToDoId == item.ToDoId)) {
                    it.Name = item.Name;
                    it.RemoteId = item.RemoteId;
                    it.UserId = item.UserId;
                    it.IsCompleted = item.IsCompleted;
                    it.IsDeleted = item.IsDeleted;
                }
            } finally {
                lockSlim.ExitWriteLock();
            }
        }

        public IList<Item> GetAll() {
            return Items.Where(item => item.IsDeleted == false).ToList();
        }

        public Item Get(int id, bool withDeleted) {
            if (withDeleted) {
                return Items.Find(item => item.ToDoId == id);
            }
            return Items.Find(item => item.ToDoId == id && item.IsDeleted == false);
        }
    }
}