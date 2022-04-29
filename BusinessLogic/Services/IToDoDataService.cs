using DataModels.Models;

namespace BusinessLogic.Services
{
    public interface IToDoDataService
    {
        void AddLists(IEnumerable<ToDoList> lists);
        Task<List<ToDoList>> GetTasks();
        void SaveChanges();
        int AddList(ToDoList list);
        int ListAddItems(int listId, List<ToDoItem> items);
        int UpdateItem(ToDoItem item);
        void Delete(int listId);
        void Delete(int listId, int itemId);
    }
}