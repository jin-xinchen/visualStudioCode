using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using TodoApi.Models;

namespace TodoApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoContext _context;  
        static ulong CountDbContext=0L;
        ulong CountDbContext1=0L;

        public TodoController(TodoContext context)
        {
            var s = Request;
            _context = context;
            System.Console.WriteLine("=Start:"+ ++CountDbContext +"==============");
            System.Console.WriteLine("=Start:"+ ++CountDbContext1 +"=======instance=======");
            if (_context.TodoItems.Count() == 0)
            {
                _context.TodoItems.Add(new TodoItem { Name = "Item1" });
                _context.SaveChanges();
            }
        }

        [HttpGet]
        // public ActionResult<List<TodoItem>> GetAll([FromQuery]object request)//) ok
        //  public ActionResult<List<TodoItem>> GetAll([FromBody]object request)//error
        //  public ActionResult<List<TodoItem>> GetAll([FromRoute]object request)//ok
        //   public ActionResult<List<TodoItem>> GetAll([FromForm]object request)//ok
        // public ActionResult<List<TodoItem>> GetAll([FromHeader]object request)//ok
        // public ActionResult<List<TodoItem>> GetAll([FromServices]object request)//error
        public ActionResult<List<TodoItem>> GetAll()
        {
            //  System.Console.WriteLine("==RequireHttpsAttribute==>:"+
            //  $"Get {Newtonsoft.Json.JsonConvert.SerializeObject(request)}"+request.ToString());
            // System.Console.WriteLine("==RequireHttpsAttribute==>:"+$"Get {Newtonsoft.Json.JsonConvert.SerializeObject(request)}");
            var s = Request;
            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public ActionResult<TodoItem> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }
            return item;
        }
        //Add the following Create method:
        [HttpPost]
        public IActionResult Create(TodoItem item)
        {
            _context.TodoItems.Add(item);
            _context.SaveChanges();
            /*
            The CreatedAtRoute method:

            Returns a 201 response. HTTP 201 is the standard response for an HTTP POST method that creates a new resource on the server.
            Adds a Location header to the response. The Location header specifies the URI of the newly created to-do item. See 10.2.2 201 Created.
            Uses the "GetTodo" named route to create the URL. The "GetTodo" named route is defined in GetById:
             */
            return CreatedAtRoute("GetTodo", new { id = item.Id }, item);
        }

        /*
        The following Update method:
        Update is similar to Create, except it uses HTTP PUT. The response is 204 (No Content). According to the HTTP specification, a PUT request requires the client to send the entire updated entity, not just the deltas. To support partial updates, use HTTP PATCH.
        */
        [HttpPut("{id}")]
        public IActionResult Update(long id, TodoItem item)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            todo.IsComplete = item.IsComplete;
            todo.Name = item.Name;

            _context.TodoItems.Update(todo);
            _context.SaveChanges();
            return NoContent();
        }
        /*
        the following Delete method.
        The Delete response is 204 (No Content).
         */
        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            var todo = _context.TodoItems.Find(id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.TodoItems.Remove(todo);
            _context.SaveChanges();
            return NoContent();
        }
        
    }
}