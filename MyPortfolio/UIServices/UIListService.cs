using ToDoList.Api.Controllers;
using DataModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;

namespace UI.UIServices
{
    public class UIListService : IUIListService
    {
        //private readonly HttpClient _httpClient;
        private readonly HttpClient apiClient;

        public UIListService(HttpClient httpClient)
        {
            this.apiClient = httpClient;
        }

        //public UIListService(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        public async Task<List<DataModels.Models.ToDoList>> GetToDoLists()
        {
            //return await _httpClient.GetFromJsonAsync<List<ToDoListModel>>("/todolist/getlist");
            return await apiClient.GetFromJsonAsync<List<DataModels.Models.ToDoList>>("api/ToDoList/GetLists");
            // "/todolist/getlist"
        }
    }
}
