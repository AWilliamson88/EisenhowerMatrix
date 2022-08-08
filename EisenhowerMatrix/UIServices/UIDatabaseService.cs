using DataModels.Models;

namespace EisenhowerMatrix.UIServices
{
    public class UIDatabaseService : IUIDatabaseService
    {
        private readonly HttpClient httpClient;

        public UIDatabaseService(HttpClient _httpClient)
        {
            this.httpClient = _httpClient;
        }

        public async Task<List<EMList>> GetLists()
        {
            var result = await httpClient.GetFromJsonAsync<List<EMList>>(
                "api/EM/GetLists"
                );

            return result;
        }

        public void Delete(int listId, int itemId)
        {
            httpClient.DeleteAsync(
                $"api/EM/Delete?listId={listId}&itemId={itemId}"
                );
        }

        public async void UpdateItem(EMListItem item)
        {
            await httpClient.PutAsJsonAsync(
                "api/EM/updateItem", item
                );
        }

        public async Task<int> AddItems(int listId, ICollection<EMListItem> items)
        {
            var result = await httpClient.PutAsJsonAsync(
                $"api/EM/AddItems?listId={listId}", items);

            return (int)result.StatusCode;
        }
    }
}
