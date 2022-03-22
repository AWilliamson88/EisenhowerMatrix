using BusinessLogic.Services;
using DataModels.Models;

namespace Api
{
    public static class DbInitialiser
    {
        public static void Initialise(IToDoDataAccessService listService)
        {
            listService.InitialiseDb();
        }
    }
}
