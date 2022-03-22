using DataModels.Models;

namespace UI.UIServices
{
    public interface IUIListService
    {
        Task<List<ToDoList>> GetToDoLists();
        void Delete(int listId);
        void Delete(int listId, int itemId);
        Task<int> AddItem(ToDoList toDoList);
    }
}