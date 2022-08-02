using DataModels.Models;

namespace EisenhowerMatrix.UIServices
{
    public class DefaultDataService
    {
        public async Task<List<ToDoList>> GetDefaultTDMatrix()
        {
            var lists = new List<ToDoList>();
            await Task.Run(() =>
            {
                lists = new List<ToDoList>
                {

                    new ToDoList {
                        Title = "Urgent & Important",
                        ToDoItems = new List<ToDoItem>()
                    },
                    new ToDoList {
                        Title = "Urgent & Not-Important",
                        ToDoItems = new List<ToDoItem>()
                    },
                    new ToDoList {
                        Title = "Not-Urgent & Important",
                        ToDoItems = new List<ToDoItem>()
                    },
                    new ToDoList {
                        Title = "Not-Urgent & Not-Important",
                        ToDoItems = new List<ToDoItem>()
                    }
                };
            });
            return lists;
        }
    }
}
