﻿using System;
using System.Configuration;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;

namespace WebApiToDoList.Services {
    /// <summary>
    /// Works with Users backend.
    /// </summary>
    public class UserService {
        /// <summary>
        /// The service URL.
        /// </summary>
        private readonly string serviceApiUrl = ConfigurationManager.AppSettings["ToDoServiceUrl"];

        /// <summary>
        /// The url for users' creation.
        /// </summary>
        private const string CreateUrl = "Users";

        private readonly HttpClient httpClient;
        
        /// <summary>
        /// Creates the service.
        /// </summary>
        public UserService() {
            httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        /// <summary>
        /// Creates a user.
        /// </summary>
        /// <param name="userName">The user name.</param>
        /// <returns>The User Id.</returns>
        public int CreateUser(string userName) {
            var response = httpClient.PostAsJsonAsync(serviceApiUrl + CreateUrl, userName).Result;
            response.EnsureSuccessStatusCode();
            return response.Content.ReadAsAsync<int>().Result;
        }

        /// <summary>
        /// Tries to take existing user from cookies. If fails then creates a new user with autogenerated name.
        /// </summary>
        /// <returns>The User Id.</returns>
        public int GetOrCreateUser() {
            var userCookie = HttpContext.Current.Request.Cookies["user"];
            int userId;

            // No user cookie or it's damaged
            if (userCookie == null || !Int32.TryParse(userCookie.Value, out userId)) {
                userId = CreateUser("Noname: " + Guid.NewGuid());

                // Store the user in a cookie for later access
                var cookie = new HttpCookie("user", userId.ToString())
                {
                    Expires = DateTime.Today.AddMonths(1)
                };

                HttpContext.Current.Response.SetCookie(cookie);
            }

            return userId;
        }
    }
}