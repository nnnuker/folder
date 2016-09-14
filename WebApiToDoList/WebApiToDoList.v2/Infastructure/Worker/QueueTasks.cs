using System.Collections.Generic;
using System.Linq;
using WebApiToDoList.v2.Infastructure.Actions;

namespace WebApiToDoList.v2.Infastructure.Worker {
    public class QueueTasks {
        private Queue<Action> Queue { get; set; }
        public static QueueTasks Instance { get; }
        private QueueTasks () {
            Queue = new Queue<Action>();
        }

        static QueueTasks () {
            Instance = new QueueTasks();
        }
        public void Enqueue(Action action) {
            Queue.Enqueue(action);
        }
        public Action Dequeue() {
            return Queue.Dequeue();
        }
        public bool IsEmpty() {
            return Queue.Count == 0;
        }
    }
}