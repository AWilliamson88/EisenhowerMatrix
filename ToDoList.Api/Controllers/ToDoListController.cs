using BusinessLogic.Services;
using DataModels.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IToDoDataAccessService toDoDb;

        public ToDoListController(IToDoDataAccessService _listService)
        {
            toDoDb = _listService;
        }

        [HttpGet]
        [Route("GetLists")]
        public IActionResult GetLists()
        {
            List<ToDoList> listModels = toDoDb.GetTasks().Result;
            return Ok(listModels);
        }

  //      [HttpDelete]
  //      [Route("Delete")]
  //      public IActionResult DeleteList(int listId)
		//{
  //          if (listId <= 0)
  //              return BadRequest("Not a valid list id");

  //          listService.Delete(listId);
  //          return Ok();
  //      }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteItem(int listId, int itemId)
        {
            if (listId <= 0 || itemId <= 0)
                return BadRequest("List or item id not valid");

            toDoDb.Delete(listId, itemId);
            return Ok();
        }

        [HttpPost]
        [Route("Put")]
        public IActionResult PutAddItems(int listId, List<ToDoItem> items)
		{   // update the list, then send the list through....
			if (!ModelState.IsValid)
				return BadRequest("Not a valid model");
            if (listId <= 0)
                return BadRequest("Not a valid list number");

            int response = toDoDb.ListAddItems(listId, items);

            return response == 0 ? Ok() : NotFound();
		}
	}
}
