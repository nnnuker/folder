using System.Collections.Generic;
using System.Web.Http;
using WebApiToDoList.v2.Infastructure.Repository;
using WebApiToDoList.v2.Infastructure.Worker;
using WebApiToDoList.v2.Models;
using WebApiToDoList.v2.Services;

namespace WebApiToDoList.v2.Controllers {
    /// <summary>
    /// Processes todo requests.
    /// </summary>
    public class ToDosController : ApiController {
        private readonly ToDoService todoService = new ToDoService();
        private readonly UserService userService = new UserService();
        private bool isInit = LocalRepository.Instance == null;

        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public IList<ToDoItemViewModel> Get() {
            var userId = userService.GetOrCreateUser();
            if (isInit) {
                return todoService.GetItems(userId, isInit);
            } else {
                return todoService.GetItems(userId, !isInit);
            }
            
        }

        /// <summary>
        /// Updates the existing todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to update.</param>
        public void Put(ToDoItemViewModel todo) {
            todo.UserId = userService.GetOrCreateUser();
            todoService.UpdateItem(todo);
        }

        /// <summary>
        /// Deletes the specified todo-item.
        /// </summary>
        /// <param name="id">The todo item identifier.</param>
        public void Delete(int id) {
            todoService.DeleteItem(id);
        }

        /// <summary>
        /// Creates a new todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to create.</param>
        public void Post(ToDoItemViewModel todo) {
            todo.UserId = userService.GetOrCreateUser();
            todoService.CreateItem(todo);
        }
    }
}
