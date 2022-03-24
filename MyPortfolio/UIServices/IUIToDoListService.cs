using DataModels.Models;

namespace UI.UIServices
{
    public interface IUIToDoListService
    {
        Task<List<ToDoList>> GetToDoLists();
        Task<int> AddItems(int listId, ICollection<ToDoItem> items);
        void UpdateItem(ToDoItem item);
        void Delete(int listId, int itemId);
    }
}