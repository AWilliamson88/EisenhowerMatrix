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

        public void InitialiseDb()
        {
            if (context.ToDoLists.Any())
            {
                return;
            }

            ToDoList list1 = new ToDoList { Title = "Urgent + Important" };
            ToDoList list2 = new ToDoList { Title = "Urgent + Not-Important" };
            ToDoList list3 = new ToDoList { Title = "Not-Urgent + Important" };
            ToDoList list4 = new ToDoList { Title = "Not-Urgent + Not-Important" };

            list1.ToDoItems.Add(new ToDoItem { Title = "Daily Planner" });
            list1.ToDoItems.Add(new ToDoItem { Title = "Get Coffee" });
            list1.ToDoItems.Add(new ToDoItem { Title = "Check Emails" });

            list2.ToDoItems.Add(new ToDoItem { Title = "Milk" });
            list2.ToDoItems.Add(new ToDoItem { Title = "Bread" });
            list2.ToDoItems.Add(new ToDoItem { Title = "Dinner" });

            context.Add(list1);
            context.Add(list2);
            context.Add(list3);
            context.Add(list4);
            context.SaveChanges();
        }

        public async Task<List<ToDoList>> GetTasks()
        {
            return await context.ToDoLists.Include(l => l.ToDoItems).Distinct().ToListAsync();
            //return await context.ToDoLists.ToListAsync();
            //.Include(l => l.ToDoItems)
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

            //var list = context.ToDoLists
            //    .Where(l => l.ToDoListId == listId)
            //    .FirstOrDefault();

            var target = context.ToDoItems
                .Where(i => i.ToDoItemId == itemId)
                .SingleOrDefault();

            context.Remove<ToDoItem>(target);
            context.SaveChanges();
        }

    }
}
