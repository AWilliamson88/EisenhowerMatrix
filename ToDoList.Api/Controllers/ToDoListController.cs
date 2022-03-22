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

            listService.Delete(listId, itemId);
            return Ok();
        }

        [HttpPost]
        [Route("Put")]
        public IActionResult PutList(ToDoList list)
		{   // update the list, then send the list through....
			if (!ModelState.IsValid)
				return BadRequest("Not a valid model");

			int response = listService.ListAddItem(list);

            return response == 0 ? Ok() : NotFound();
		}
	}
}
