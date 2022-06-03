using DataModels.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic.Services
{
    public class ToDoDataService : IToDoDataService
    {
        private readonly PortfolioContext context;
        public ToDoDataService(PortfolioContext _context)
        {
            context = _context;
        }

        public async Task<List<ToDoList>> GetTasks()
        {
            return await context.ToDoLists.Include(l => l.ToDoItems).Distinct().ToListAsync();
        }

        public async Task<int> ListAddItems(int listId, IEnumerable<ToDoItem> items)
		{
            var list = context.ToDoLists
                .Where(l => l.ToDoListId == listId)
                .Single();

            list.ToDoItems = (ICollection<ToDoItem>)items;

            context.Update(list);
            return await context.SaveChangesAsync();
		}

        public async Task<int> UpdateItem(ToDoItem item)
        {
            if (item == null)
                return 0;

            context.Update(item);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(int listId, int itemId)
        {
            var target = context.ToDoItems
                .Where(i => i.ToDoItemId == itemId)
                .SingleOrDefault();

            context.Remove<ToDoItem>(target);
            return await context.SaveChangesAsync();
        }

    }
}
