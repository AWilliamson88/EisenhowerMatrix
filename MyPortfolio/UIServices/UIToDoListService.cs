using DataModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Web.Http;

namespace UI.UIServices
{
    public class UIToDoListService : IUIToDoListService
    {
        //private readonly HttpClient _httpClient;
        private readonly HttpClient httpClient;

        public UIToDoListService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }

        //public UIListService(HttpClient httpClient)
        //{
        //    _httpClient = httpClient;
        //}

        public async Task<List<ToDoList>> GetToDoLists()
        {
            //return await _httpClient.GetFromJsonAsync<List<ToDoListModel>>("/todolist/getlist");
            return await httpClient.GetFromJsonAsync<List<ToDoList>>("api/ToDoList/GetLists");
            // "/todolist/getlist"
        }

        public void Delete(int listId)
        {
            //apiClient.PostAsJsonAsync("api/ToDoList", item);
            //httpClient.DeleteAsync("api/ToDoList/Delete/" + listId.ToString());

        }

        public void Delete(int listId, int itemId)
        {
            httpClient.DeleteAsync(
                "api/ToDoList/Delete?listId=" + listId + 
                "&itemId=" + itemId);
        }

        public async void UpdateItem(ToDoItem item)
        {
            await httpClient.PutAsJsonAsync("api/ToDoList/PutItem", item);
        }

        public async Task<int> AddItems(int listId, ICollection<ToDoItem> items)
        {
            var result = await httpClient.PutAsJsonAsync($"api/ToDoList/PutItems?listId={listId}", items);

            return (int)result.StatusCode;
        }

        public void AddList()
		{

		}

    }
}
