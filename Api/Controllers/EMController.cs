using BusinessLogic.Services;
using DataModels.Models;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EMController : ControllerBase
    {
        private readonly IEMDataService DbService;

        public EMController(IEMDataService service)
        {
            DbService = service;
        }

        [HttpGet]
        [Route("GetLists")]
        public async Task<ActionResult<IEnumerable<EMList>>> GetLists()
        {
            IEnumerable<EMList> listModels = await DbService.GetTasks();
            if (!listModels.Any())
                return NotFound();

            return Ok(listModels);
        }

        [HttpDelete]
        [Route("Delete")]
        public async Task<ActionResult> DeleteItem([FromQuery] int listId, int itemId)
        {
            if (listId <= 0 || itemId <= 0)
                return BadRequest("List or item id not valid");

            int result = await DbService.Delete(listId, itemId);
            return result == 0 ? NotFound("Item not found") : NoContent();
        }

        [HttpPut]
        [Route("AddItems")]
        public async Task<ActionResult> AddItems(int listId, IEnumerable<EMListItem> items)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");
            if (listId <= 0)
                return BadRequest("Not a valid list number");

            int response = await DbService.ListAddItems(listId, items);
            return response > 0 ? Ok() : NotFound();
        }

        [HttpPut]
        [Route("UpdateItem")]
        public async Task<ActionResult> UpdateItem(EMListItem item)
        {
            if (!ModelState.IsValid || item == null)
                return BadRequest("Not a valid todo item");

            int response = await DbService.UpdateItem(item);
            return response > 0 ? Ok() : NotFound();
        }

    }
}
