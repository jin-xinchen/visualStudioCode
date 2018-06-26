using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;
using TodoApi.Helper;

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
            context.MyTest=9;
            System.Console.WriteLine($"DbContextOptions<TodoContext> options==>{context.GetHashCode()} {context.GetType().FullName}");
            var s = Request;
            _context = context;
            
            // System.Console.WriteLine("=Start:"+ ++CountDbContext +"==============");
            // Console.WriteLine();
            // System.Console.WriteLine("=Start:"+ ++CountDbContext1 +"=======instance=======");
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
            var sRequest = this.HttpContext.Request;
            var rH= sRequest.Headers;
            // Console.WriteLine();
            // Console.WriteLine($"Method=={sRequest.Method} ");
            // Console.WriteLine($"Protocol=={sRequest.Protocol} ");
            // foreach(var sHead in rH)
            // {
            //    Console.WriteLine($"{sHead.Key}=={sHead.Value}");
            // }

            // Console.WriteLine(Utils.GetJSONofHeader(sRequest));

            return _context.TodoItems.ToList();
        }

        [HttpGet("{id}", Name = "GetTodo")]
        public async Task<ActionResult<TodoItem>> GetById(long id)
        {
            var item = _context.TodoItems.Find(id);
            if (item == null)
            {
                return NotFound();
            }

            int contentLength = await this.AccessTheWebAsync();  
            // Console.WriteLine("4====>"+contentLength);
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

        // Three things to note in the signature:  
        //  - The method has an async modifier.   
        //  - The return type is Task or Task<T>. (See "Return Types" section.)  
        //    Here, it is Task<int> because the return statement returns an integer.  
        //  - The method name ends in "Async."  
        async Task<int> AccessTheWebAsync()  
        {
            // You need to add a reference to System.Net.Http to declare client.  
            System.Net.Http.HttpClient client = new System.Net.Http.HttpClient();  

            string url0 ="http://docs.microsoft.com";
            // Console.WriteLine($"20=== characters"); 
            byte[] urlContents1 = await client.GetByteArrayAsync(url0);
            // Console.WriteLine($"21==={url0}: {urlContents1.Length/2:N0} characters"); 

            string url ="http://msdn.microsoft.com"; 
            // GetStringAsync returns a Task<string>. That means that when you await the  
            // task you'll get a string (urlContents).  
            Task<string> getStringTask = client.GetStringAsync(url);  
            var uri = new Uri(Uri.EscapeUriString(url));
            // You can do work here that doesn't rely on the string from GetStringAsync.  
            DoIndependentWork(); 
             
            // The await operator suspends AccessTheWebAsync.  
            //  - AccessTheWebAsync can't continue until getStringTask is complete.  
            //  - Meanwhile, control returns to the caller of AccessTheWebAsync.  
            //  - Control resumes here when getStringTask is complete.   
            //  - The await operator then retrieves the string result from getStringTask.  
            string urlContents = await getStringTask;  
            // Console.WriteLine("2====>"+urlContents.ToString().Substring(0,16));
            // Console.WriteLine();
            // Console.WriteLine("3====>"+getStringTask.ToString());
            // Console.WriteLine();
            // The return statement specifies an integer result.  
            // Any methods that are awaiting AccessTheWebAsync retrieve the length value.  
            return urlContents.Length;  
        }  

        void DoIndependentWork()  
        {  
            string resultsTextBox = "Working . . . . . . .\r\n";  
            // Console.WriteLine();
            // Console.WriteLine("1====>"+resultsTextBox);
            // Console.WriteLine();


        } 
        
    }
}