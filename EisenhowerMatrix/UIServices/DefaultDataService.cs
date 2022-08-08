using DataModels.Models;

namespace EisenhowerMatrix.UIServices
{
    public class DefaultDataService
    {
        public async Task<List<EMList>> GetDefaultData()
        {
            var lists = new List<EMList>();
            await Task.Run(() =>
            {
                lists = new List<EMList>
                {

                    new EMList {
                        Title = "Urgent & Important",
                        EMListItems = new List<EMListItem>()
                    },
                    new EMList {
                        Title = "Urgent & Not-Important",
                        EMListItems = new List<EMListItem>()
                    },
                    new EMList {
                        Title = "Not-Urgent & Important",
                        EMListItems = new List<EMListItem>()
                    },
                    new EMList {
                        Title = "Not-Urgent & Not-Important",
                        EMListItems = new List<EMListItem>()
                    }
                };
            });
            return lists;
        }
    }
}
