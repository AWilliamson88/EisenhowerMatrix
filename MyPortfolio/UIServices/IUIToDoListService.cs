using DataModels.Models;

namespace UI.UIServices
{
    public interface IUIToDoListService
    {
        Task<List<ToDoList>> GetToDoLists();
        void Delete(int listId);
        void Delete(int listId, int itemId);
        Task<int> AddItems(int listId, ICollection<ToDoItem> items);
    }
}