using BusinessLogic.Services;
using DataModels.Models;

namespace Api
{
    public static class DbInitialiser
    {
        public static void Initialise(IToDoDataService listService)
        {
            listService.InitialiseDb();
        }
    }
}
