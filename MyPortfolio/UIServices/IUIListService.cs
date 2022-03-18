using DataModels.Models;

namespace UI.UIServices
{
    public interface IUIListService
    {
        Task<List<DataModels.Models.ToDoList>> GetToDoLists();
    }
}