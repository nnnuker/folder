using System.Threading;
using System.Threading.Tasks;
using WebApiToDoList.v2.Infastructure.Actions;

namespace WebApiToDoList.v2.Infastructure.Worker {
    public class Worker {
        private static QueueTasks Queue { get; set; }
        public static Worker Instance { get; }
        private static readonly ReaderWriterLockSlim lockSlim = new ReaderWriterLockSlim();

        private Worker () {
            Queue =  QueueTasks.Instance;
        }

        static Worker () {
            Instance = new Worker();
        }


        public void Run() {
            while (true) {
                lockSlim.EnterWriteLock();
                try {
                    if (!Queue.IsEmpty()) {
                        var task = Queue.Dequeue();
                        task.Do();
                        Thread.Sleep(5);
                    }
                } finally {
                        lockSlim.ExitWriteLock();
                }
            }
        }

        public static void AddWork(Action action) => Queue.Enqueue(action);
         
    }
}