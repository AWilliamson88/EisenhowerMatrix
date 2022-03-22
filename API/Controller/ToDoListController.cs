using BusinessLogic.Services;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace API.Controller
{
    [ApiController]
    public class ToDoListController : ControllerBase
    {
        private readonly IListService _listService;

        public ToDoListController(IListService listService)
        {
            _listService = listService;
        }

        [HttpGet]
        [Route("GetList")]
        public IActionResult GetLists()
        {
            var data = _listService.GetTasks();
            return Ok(data);
        }

		//// GET: api/<ToDoListController>
		//[HttpGet]
		//public IEnumerable<string> Get()
		//{
		//    return new string[] { "value1", "value2" };
		//}

		//// GET api/<ToDoListController>/5
		//[HttpGet("{id}")]
		//public string Get(int id)
		//{
		//    return "value";
		//}

		//POST api/<ToDoListController>
		[HttpPost]
		public void Post([FromBody] string value)
		{

		}

		//// PUT api/<ToDoListController>/5
		//[HttpPut("{id}")]
		//public void Put(int id, [FromBody] string value)
		//{
		//}

		//// DELETE api/<ToDoListController>/5
		//[HttpDelete("{id}")]
		//public void Delete(int id)
		//{
		//}
	}
}
