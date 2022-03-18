using DataModels.Models;

namespace BusinessLogic.Services
{
    public interface IListService
    {
        void InitialiseDb();
        void AddLists(IEnumerable<ToDoList> lists);
        Task<List<ToDoList>> GetTasks();
        void SaveChanges();
    }
}