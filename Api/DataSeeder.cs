using DataModels.Models;
using Microsoft.EntityFrameworkCore;

namespace Api
{
    public class DataSeeder : IDataSeeder
    {
        private EMDbContext Context { get; }

        public DataSeeder(EMDbContext context)
        {
            Context = context;
        }

        public void Seed()
        {
            if (Context.EMLists.Count() / 4 != 1)
            {
                var lists = new List<EMList>()
                {
                        new EMList()
                        {
                            Title = "Urgent & Important",
                            EMListItems = new List<EMListItem>()
                            {
                                new EMListItem()
                                {
                                    Title = "Update schedule",
                                    Description = "check last weeks and next weeks too."
                                },
                                new EMListItem()
                                {
                                    Title = "Check emails"
                                }
                            }
                        },
                        new EMList()
                        {
                            Title = "Urgent & Not-Important",
                            EMListItems = new List<EMListItem>()
                            {
                                new EMListItem()
                                {
                                    Title = "Food shopping"
                                }
                            }
                        },
                        new EMList()
                        {
                            Title = "Not-Urgent & Important",
                            EMListItems = new List<EMListItem>()
                            {
                                new EMListItem()
                                {
                                    Title = "Hair cut"
                                }
                            }
                        },
                        new EMList()
                        {
                            Title = "Not-Urgent & Not-Important",
                            EMListItems = new List<EMListItem>()
                            {
                                new EMListItem()
                                {
                                    Title = "Items Placed Here Should Be Deleted."
                                }
                            }
                        },

                };

                Context.EMLists.RemoveRange(Context.EMLists.Include(l => l.EMListItems));
                Context.SaveChanges();
                Context.EMLists.AddRange(lists);
                Context.SaveChanges();
            }
        }

    }
}
