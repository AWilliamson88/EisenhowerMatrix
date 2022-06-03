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
        public async Task<ActionResult<IEnumerable<ToDoList>>> GetLists()
        {
            IEnumerable<ToDoList> listModels = await toDoDbService.GetTasks();
            if (!listModels.Any())
                listModels = new List<ToDoList> { new ToDoList() };

            return Ok(listModels);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> DeleteItem([FromQuery] int listId, int itemId)
        {
            if (listId <= 0 || itemId <= 0)
                return BadRequest("List or item id not valid");

            int result = await toDoDbService.Delete(listId, itemId);
            return result == 0 ? NotFound("Item not found") : NoContent();
        }

        [HttpPut]
        [Route("PutItems")]
        public async Task<ActionResult> PutAddItems(int listId, IEnumerable<ToDoItem> items)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            if (listId <= 0)
                return BadRequest("Not a valid list number");

            int response = await toDoDbService.ListAddItems(listId, items);
            return response > 0 ? Ok() : NotFound();
        }

        [HttpPut]
        [Route("PutItem")]
        public async Task<ActionResult> UpdateItem(ToDoItem item)
        {
            if (!ModelState.IsValid || item == null)
                return BadRequest("Not a valid todo item");

            int response = await toDoDbService.UpdateItem(item);
            return response > 0 ? Ok() : NotFound();
        }

    }
}
