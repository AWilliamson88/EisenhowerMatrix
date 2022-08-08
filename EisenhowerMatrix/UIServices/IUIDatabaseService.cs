using DataModels.Models;

namespace EisenhowerMatrix.UIServices
{
    public interface IUIDatabaseService
    {
        Task<List<EMList>> GetLists();
        Task<int> AddItems(int listId, ICollection<EMListItem> items);
        void UpdateItem(EMListItem item);
        void Delete(int listId, int itemId);
    }
}