using BusinessLogic.Services;
using DataModels.Models;

namespace Api
{
    public static class DbInitialiser
    {
        public static void Initialise(IListService listService)
        {
            listService.InitialiseDb();
        }
    }
}
