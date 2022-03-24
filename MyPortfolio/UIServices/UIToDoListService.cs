using DataModels.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Web.Http;

namespace UI.UIServices
{
    public class UIToDoListService : IUIToDoListService
    {
        private readonly HttpClient httpClient;

        public UIToDoListService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }

        public async Task<List<ToDoList>> GetToDoLists()
        {
            return await httpClient.GetFromJsonAsync<List<ToDoList>>("api/ToDoList/GetLists");
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

    }
}
