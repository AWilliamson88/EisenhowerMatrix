using DataModels.Models;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogic.Services
{
    public class EMDataService : IEMDataService
    {
        private readonly EMDbContext context;
        public EMDataService(EMDbContext _context)
        {
            context = _context;
        }

        public async Task<IEnumerable<EMList>> GetTasks()
        {
            return await context.EMLists
                .Include(l => l.EMListItems)
                .Distinct()
                .ToListAsync();
        }

        public async Task<int> ListAddItems(int listId, IEnumerable<EMListItem> items)
        {
            var list = context.EMLists
                .Where(l => l.EMListId == listId)
                .Single();

            list.EMListItems = (ICollection<EMListItem>)items;

            context.Update(list);
            return await context.SaveChangesAsync();
        }

        public async Task<int> UpdateItem(EMListItem item)
        {
            if (item == null)
                return 0;

            context.Update(item);
            return await context.SaveChangesAsync();
        }

        public async Task<int> Delete(int listId, int itemId)
        {
            var target = context.EMListItems
                .Where(i => i.EMListItemId == itemId)
                .SingleOrDefault();

            context.Remove<EMListItem>(target);
            return await context.SaveChangesAsync();
        }

    }
}
