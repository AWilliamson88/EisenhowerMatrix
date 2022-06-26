using DataModels.Models;

namespace BusinessLogic.Services
{
    public interface IToDoDataService
    {
        Task<IEnumerable<ToDoList>> GetTasks();
        Task<int> ListAddItems(int listId, IEnumerable<ToDoItem> items);
        Task<int> UpdateItem(ToDoItem item);
        Task<int> Delete(int listId, int itemId);
    }
}