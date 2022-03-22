using DataModels.Models;

namespace BusinessLogic.Services
{
    public interface IToDoDataAccessService
    {
        void InitialiseDb();
        void AddLists(IEnumerable<ToDoList> lists);
        Task<List<ToDoList>> GetTasks();
        void SaveChanges();
        void Delete(int listId);
        void Delete(int listId, int itemId);
        int AddList(ToDoList list);
        int ListAddItems(int listId, List<ToDoItem> items);
    }
}