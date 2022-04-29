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

        public void AddLists(IEnumerable<ToDoList> lists)
        {
            context.ToDoLists.AddRange(lists);
        }

        public void SaveChanges()
        {
            context.SaveChanges();
        }

        public int ListAddItems(int listId, List<ToDoItem> items)
		{
            var list = context.ToDoLists
                .Where(l => l.ToDoListId == listId)
                .Single();

            list.ToDoItems = items;

            context.Update(list);
            return context.SaveChanges();
		}

        public int AddList(ToDoList list)
        {
            context.ToDoLists.Add(list);
            return context.SaveChanges();
        }

        public int UpdateItem(ToDoItem item)
        {
            if (item == null)
                return 0;

            context.Update(item);
            return context.SaveChanges();
        }

        public void Delete(int listId)
		{
            var target = context.ToDoLists
                .Where(i => i.ToDoListId == listId)
                .SingleOrDefault();

            context.Remove<ToDoList>(target);
            context.SaveChanges();
        }

        public void Delete(int listId, int itemId)
        {
            var target = context.ToDoItems
                .Where(i => i.ToDoItemId == itemId)
                .SingleOrDefault();

            context.Remove<ToDoItem>(target);
            context.SaveChanges();
        }

    }
}
