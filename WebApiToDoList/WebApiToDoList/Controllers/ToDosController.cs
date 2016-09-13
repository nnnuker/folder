using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web.Http;
using WebApiToDoList.Models;
using WebApiToDoList.Services;

namespace WebApiToDoList.Controllers {
    /// <summary>
    /// Processes todo requests.
    /// </summary>
    public class ToDosController : ApiController {
        private readonly ToDoService _todoService = new ToDoService();
        private readonly UserService _userService = new UserService();

        /// <summary>
        /// Returns all todo-items for the current user.
        /// </summary>
        /// <returns>The list of todo-items.</returns>
        public async Task<IList<ToDoItemViewModel>> Get() {
            var userId = await _userService.GetOrCreateUser();
            return await _todoService.GetItems(userId);
        }

        /// <summary>
        /// Updates the existing todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to update.</param>
        public async Task Put(ToDoItemViewModel todo) {
            todo.UserId = await _userService.GetOrCreateUser();
            await _todoService.UpdateItem(todo);
        }

        /// <summary>
        /// Deletes the specified todo-item.
        /// </summary>
        /// <param name="id">The todo item identifier.</param>
        public async Task Delete(int id) {
            await _todoService.DeleteItem(id);
        }

        /// <summary>
        /// Creates a new todo-item.
        /// </summary>
        /// <param name="todo">The todo-item to create.</param>
        public async Task Post(ToDoItemViewModel todo) {
            todo.UserId = await _userService.GetOrCreateUser();
            await _todoService.CreateItem(todo);
        }
    }
}
