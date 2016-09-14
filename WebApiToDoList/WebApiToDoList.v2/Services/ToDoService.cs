using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using WebApiToDoList.v2.Infastructure.Mappers;
using WebApiToDoList.v2.Infastructure.Services;
using WebApiToDoList.v2.Models;

namespace WebApiToDoList.v2.Services {
    /// <summary>
    /// Works with ToDo backend.
    /// </summary>
    public class ToDoService {
        private const string GetAllUrl = "ToDos?userId={0}";
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        private readonly IService service;

        /// <summary>
        /// Creates the service.
        /// </summary>
        public ToDoService() {
            service = Service.Instance;
        }

        /// <summary>
        /// Gets all todos for the user.
        /// </summary>
        /// <param name="userId">The User Id.</param>
        /// <returns>The list of todos.</returns>
        public IList<ToDoItemViewModel> GetItems(int userId, bool isInit) {
            if (isInit)
                return service.GetAll().Select(item => item.ToViewModel()).ToList();
            else {
                var httpClient = new HttpClient();
                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var dataAsString = httpClient.GetStringAsync(string.Format(serviceApiUrl + GetAllUrl, userId)).Result;
                return JsonConvert.DeserializeObject<IList<ToDoItemViewModel>>(dataAsString);
            }
        }

        /// <summary>
        /// Creates a todo. UserId is taken from the model.
        /// </summary>
        /// <param name="item">The todo to create.</param>
        public void CreateItem(ToDoItemViewModel item) {
            service.Add(item.ToItem());
//            httpClient.PostAsJsonAsync(serviceApiUrl + CreateUrl, item)
//                .Result.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Updates a todo.
        /// </summary>
        /// <param name="item">The todo to update.</param>
        public void UpdateItem(ToDoItemViewModel item) {
            service.Update(item.ToItem());
//            httpClient.PutAsJsonAsync(serviceApiUrl + UpdateUrl, item)
//                .Result.EnsureSuccessStatusCode();
        }

        /// <summary>
        /// Deletes a todo.
        /// </summary>
        /// <param name="id">The todo Id to delete.</param>
        public void DeleteItem(int id) {
            service.Delete(id);
        }
    }
}