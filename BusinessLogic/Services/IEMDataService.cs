using DataModels.Models;

namespace BusinessLogic.Services
{
    public interface IEMDataService
    {
        Task<IEnumerable<EMList>> GetTasks();
        Task<int> ListAddItems(int listId, IEnumerable<EMListItem> items);
        Task<int> UpdateItem(EMListItem item);
        Task<int> Delete(int listId, int itemId);
    }
}