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
        private readonly IToDoDataService toDoDbService;

        public ToDoListController(IToDoDataService _listService)
        {
            toDoDbService = _listService;
        }

        [HttpGet]
        [Route("GetLists")]
        public IActionResult GetLists()
        {
            List<ToDoList> listModels = toDoDbService.GetTasks().Result;

            if (listModels == null)
                listModels = new List<ToDoList> { new ToDoList() };

            return Ok(listModels);
        }

        [HttpDelete]
        [Route("Delete")]
        public IActionResult DeleteItem(int listId, int itemId)
        {
            if (listId <= 0 || itemId <= 0)
                return BadRequest("List or item id not valid");

            toDoDbService.Delete(listId, itemId);
            return Ok();
        }

        [HttpPut]
        [Route("PutItems")]
        public IActionResult PutAddItems(int listId, List<ToDoItem> items)
		{   // update the list, then send the list through....
			if (!ModelState.IsValid)
				return BadRequest("Not a valid model");
            if (listId <= 0)
                return BadRequest("Not a valid list number");

            int response = toDoDbService.ListAddItems(listId, items);
            return response > 0 ? Ok() : NotFound();
		}

        [HttpPut]
        [Route("PutItem")]
        public IActionResult UpdateItem(ToDoItem item)
        {
            if (!ModelState.IsValid || item == null)
                return BadRequest("Not a valid todo item");

            int response = toDoDbService.UpdateItem(item);
            return response > 0 ? Ok() : NotFound();
        }

	}
}
