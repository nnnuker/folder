using System.Collections.Generic;
using System.Linq;
using WebApiToDoList.v2.Infastructure.Actions;

namespace WebApiToDoList.v2.Infastructure.Worker {
    public class QueueTasks {
        private readonly Queue<Action> queue = new Queue<Action>();
        //var someQueue
        public void Enqueue(Action action) {
            queue.Enqueue(action);
        }
        public Action Dequeue() {
            return queue.Dequeue();
        }
        public bool IsEmpty() {
            return queue.Count() == 0;
        }
    }
}