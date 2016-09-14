
using System.Threading;
using System.Threading.Tasks;

namespace WebApiToDoList.v2.Infastructure.Worker {
    //TODO: example
    public static class Worker {
        public static QueueTasks Query = new QueueTasks();
        //TODO: use lock 
        public static async Task Run() {
            while (true) {
                if (!Query.IsEmpty()) {
                   var task = Query.Dequeue();
                    await Task.Run(() => { task.Do(); });
                }
                Thread.Sleep(50);
            }
        }
         
    }
}