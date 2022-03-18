using BusinessLogic.Services;
using DataModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ToDoList.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IListService listService;

        public ToDoListController(IListService _listService)
        {
            listService = _listService;
        }

        [HttpGet]
        [Route("GetLists")]
        public IActionResult GetLists()
        {
            List<DataModels.Models.ToDoList> listModels = listService.GetTasks().Result;
            return Ok(listModels);
        }
    }
}
