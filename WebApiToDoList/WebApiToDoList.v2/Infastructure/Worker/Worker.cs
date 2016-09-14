using System.Threading;
using System.Threading.Tasks;
using WebApiToDoList.v2.Infastructure.Actions;

namespace WebApiToDoList.v2.Infastructure.Worker {
    //TODO: example
    public class Worker {
        private static QueueTasks Query = new QueueTasks();
        private static readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();

        //TODO: use lock 
        public void Run() {
            while (true) {
                lockSlim.EnterWriteLock();
                try {
                    if (!Query.IsEmpty()) {
                        var task = Query.Dequeue();
                        Task.Run(() => { task.Do(); });
                        Thread.Sleep(5);
                    }
                } finally {
                        lockSlim.ExitWriteLock();
                }
            }
        }

        public static void AddWork(Action action) => Query.Enqueue(action);
         
    }
}